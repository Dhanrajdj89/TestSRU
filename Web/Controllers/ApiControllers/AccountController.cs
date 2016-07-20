using SportsRUsApp.Core.Constants;
using SportsRUsApp.Core.DataModel;
using SportsRUsApp.Core.DataModel.Enums;
using SportsRUsApp.Core.Events;
using SportsRUsApp.Core.Interfaces.Services;
using SportsRUsApp.Core.Interfaces.UnitOfWork;
using SportsRUsApp.Utilities;
using SportsRUsApp.ViewModels.Application;
using SportsRUsApp.Web.Application;
using SportsRUsApp.Web.Areas.Admin.ViewModels;
using SportsRUsApp.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Security;
using MembershipCreateStatus = SportsRUsApp.Core.DataModel.MembershipCreateStatus;
using MembershipUser = SportsRUsApp.Core.DataModel.MembershipUser;

namespace SportsRUsApp.Web.Controllers.ApiControllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : BaseApiController
    {
        protected IEmailService _emailService { get; set; } = ServiceFactory.Get<IEmailService>();
        protected IBannedWordService _bannedWordService { get; set; } = ServiceFactory.Get<IBannedWordService>();
        protected IBannedEmailService _bannedEmailService { get; set; } = ServiceFactory.Get<IBannedEmailService>();

        /// <summary>
        /// Add a new user
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Register")]
        [ValidateAntiForgeryToken]
        public object Register(MemberAddViewModel userModel)
        {
            var msg = new GenericMessageViewModel
            {
                MessageType = GenericMessages.error,
                Message = LocalizationService.GetResourceString("Errors.GenericMessage ")
            };

            if (SettingsService.GetSettings().SuspendRegistration != true && SettingsService.GetSettings().DisableStandardRegistration != true)
            {
                using (UnitOfWorkManager.NewUnitOfWork())
                {
                    // First see if there is a spam question and if so, the answer matches
                    if (!string.IsNullOrEmpty(SettingsService.GetSettings().SpamQuestion))
                    {
                        // There is a spam question, if answer is wrong return with error
                        if (userModel.SpamAnswer == null || userModel.SpamAnswer.Trim() != SettingsService.GetSettings().SpamAnswer)
                        {
                            // POTENTIAL SPAMMER!
                            msg.Message = LocalizationService.GetResourceString("Error.WrongAnswerRegistration");
                        }
                    }

                    // Secondly see if the email is banned
                    if (_bannedEmailService.EmailIsBanned(userModel.Email))
                    {
                        msg.Message = LocalizationService.GetResourceString("Error.EmailIsBanned");
                    }
                }

                // Standard Login
                userModel.LoginType = LoginType.Standard;

                // Do the register logic
                return MemberRegisterLogic(userModel);
            }
            return msg;
        }

        private GenericMessageViewModel MemberRegisterLogic(MemberAddViewModel userModel)
        {
            var msg = new GenericMessageViewModel
            {
                MessageType = GenericMessages.error,
                Message = LocalizationService.GetResourceString("Errors.GenericMessage")
            };

            using (var unitOfWork = UnitOfWorkManager.NewUnitOfWork())
            {
                var settings = SettingsService.GetSettings();
                var manuallyAuthoriseMembers = settings.ManuallyAuthoriseNewMembers;
                var memberEmailAuthorisationNeeded = settings.NewMemberEmailConfirmation == true;
                var homeRedirect = false;

                var userToSave = new MembershipUser
                {
                    UserName = _bannedWordService.SanitiseBannedWords(userModel.UserName),
                    Email = userModel.Email,
                    Password = userModel.Password,
                    IsApproved = userModel.IsApproved,
                    Comment = userModel.Comment,
                };

                var createStatus = MembershipService.CreateUser(userToSave);
                if (createStatus != MembershipCreateStatus.Success)
                {
                    msg.Message = MembershipService.ErrorCodeToString(createStatus);
                }
                else
                {
                    // See if this is a social login and we have their profile pic
                    if (!string.IsNullOrEmpty(userModel.SocialProfileImageUrl))
                    {
                        // We have an image url - Need to save it to their profile
                        var image = AppHelpers.GetImageFromExternalUrl(userModel.SocialProfileImageUrl);

                        // Set upload directory - Create if it doesn't exist
                        var uploadFolderPath = HostingEnvironment.MapPath(string.Concat(SiteConstants.Instance.UploadFolderPath, userToSave.Id));
                        if (uploadFolderPath != null && !Directory.Exists(uploadFolderPath))
                        {
                            Directory.CreateDirectory(uploadFolderPath);
                        }

                        // Get the file name
                        var fileName = Path.GetFileName(userModel.SocialProfileImageUrl);

                        // Create a HttpPostedFileBase image from the C# Image
                        using (var stream = new MemoryStream())
                        {
                            // Microsoft doesn't give you a file extension - See if it has a file extension
                            // Get the file extension
                            var fileExtension = Path.GetExtension(fileName);

                            // Fix invalid Illegal characters
                            var regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
                            var reg = new Regex($"[{Regex.Escape(regexSearch)}]");
                            fileName = reg.Replace(fileName, "");

                            if (string.IsNullOrEmpty(fileExtension))
                            {
                                // no file extension so give it one
                                fileName = string.Concat(fileName, ".jpg");
                            }

                            image.Save(stream, ImageFormat.Jpeg);
                            stream.Position = 0;
                            HttpPostedFileBase formattedImage = new MemoryFile(stream, "image/jpeg", fileName);

                            // Upload the file
                            var uploadResult = AppHelpers.UploadFile(formattedImage, uploadFolderPath, LocalizationService, true);

                            // Don't throw error if problem saving avatar, just don't save it.
                            if (uploadResult.UploadSuccessful)
                            {
                                userToSave.Avatar = uploadResult.UploadedFileName;
                            }
                        }
                    }

                    // Store access token for social media account in case we want to do anything with it
                    var isSocialLogin = false;
                    if (userModel.LoginType == LoginType.Facebook)
                    {
                        userToSave.FacebookAccessToken = userModel.UserAccessToken;
                        isSocialLogin = true;
                    }
                    if (userModel.LoginType == LoginType.Google)
                    {
                        userToSave.GoogleAccessToken = userModel.UserAccessToken;
                        isSocialLogin = true;
                    }
                    if (userModel.LoginType == LoginType.Microsoft)
                    {
                        userToSave.MicrosoftAccessToken = userModel.UserAccessToken;
                        isSocialLogin = true;
                    }

                    // If this is a social login, and memberEmailAuthorisationNeeded is true then we need to ignore it
                    // and set memberEmailAuthorisationNeeded to false because the email addresses are validated by the social media providers
                    if (isSocialLogin && !manuallyAuthoriseMembers)
                    {
                        memberEmailAuthorisationNeeded = false;
                        userToSave.IsApproved = true;
                    }

                    // Set the view bag message here
                    msg = SetRegisterViewBagMessage(manuallyAuthoriseMembers, memberEmailAuthorisationNeeded, userToSave);
                    if (!manuallyAuthoriseMembers && !memberEmailAuthorisationNeeded)
                    {
                        homeRedirect = true;
                    }

                    try
                    {
                        // Only send the email if the admin is not manually authorising emails or it's pointless
                        SendEmailConfirmationEmail(userToSave);
                        unitOfWork.Commit();

                        if (homeRedirect)
                        {
                            return msg;
                        }
                    }
                    catch (Exception ex)
                    {
                        unitOfWork.Rollback();
                        LoggingService.Error(ex);
                        FormsAuthentication.SignOut();
                    }
                }
            }

            return msg;
        }

        private GenericMessageViewModel SetRegisterViewBagMessage(bool manuallyAuthoriseMembers, bool memberEmailAuthorisationNeeded, MembershipUser userToSave)
        {
            var msg = new GenericMessageViewModel();
            msg.MessageType = GenericMessages.success;

            if (manuallyAuthoriseMembers)
            {
                msg.Message = LocalizationService.GetResourceString("Members.NowRegisteredNeedApproval");
            }
            else if (memberEmailAuthorisationNeeded)
            {
                msg.Message = LocalizationService.GetResourceString("Members.MemberEmailAuthorisationNeeded");
            }
            else
            {
                // If not manually authorise then log the user in
                FormsAuthentication.SetAuthCookie(userToSave.UserName, false);
                msg.Message = LocalizationService.GetResourceString("Members.NowRegistered");
            }
            return msg;
        }

        private void SendEmailConfirmationEmail(MembershipUser userToSave)
        {
            var settings = SettingsService.GetSettings();
            var manuallyAuthoriseMembers = settings.ManuallyAuthoriseNewMembers;
            var memberEmailAuthorisationNeeded = settings.NewMemberEmailConfirmation == true;
            if (manuallyAuthoriseMembers == false && memberEmailAuthorisationNeeded)
            {
                if (!string.IsNullOrEmpty(userToSave.Email))
                {
                    // SEND AUTHORISATION EMAIL
                    var sb = new StringBuilder();
                    var confirmationLink = string.Concat(StringUtils.ReturnCurrentDomain(), "EmailConfirmation/" + userToSave.Id);
                    sb.AppendFormat("<p>{0}</p>", string.Format(LocalizationService.GetResourceString("Members.MemberEmailAuthorisation.EmailBody"),
                                                settings.ForumName, string.Format("<p><a href=\"{0}\">{0}</a></p>", confirmationLink)));
                    var email = new Email
                    {
                        EmailTo = userToSave.Email,
                        NameTo = userToSave.UserName,
                        Subject = LocalizationService.GetResourceString("Members.MemberEmailAuthorisation.Subject")
                    };
                    email.Body = _emailService.EmailTemplate(email.NameTo, sb.ToString());
                    _emailService.SendMail(email);

                    // ADD COOKIE
                    // We add a cookie for 7 days, which will display the resend email confirmation button
                    // This cookie is removed when they click the confirmation link
                    var myCookie = new HttpCookie(AppConstants.MemberEmailConfirmationCookieName)
                    {
                        Value = $"{userToSave.Email}#{userToSave.UserName}",
                        Expires = DateTime.UtcNow.AddDays(7)
                    };
                    // Add the cookie.
                    HttpContext.Current.Response.Cookies.Add(myCookie);
                }
            }
        }

        public object ResendEmailConfirmation(string username)
        {
            var msg = new GenericMessageViewModel
            {
                MessageType = GenericMessages.error,
                Message = LocalizationService.GetResourceString("Errors.GenericMessage")
            };

            using (var unitOfWork = UnitOfWorkManager.NewUnitOfWork())
            {
                var user = MembershipService.GetUser(username);
                if (user != null)
                {
                    SendEmailConfirmationEmail(user);
                    msg.Message = LocalizationService.GetResourceString("Members.MemberEmailAuthorisationNeeded");
                    msg.MessageType = GenericMessages.success;
                }

                try
                {
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    LoggingService.Error(ex);
                }
            }
            return msg;
        }

        /// <summary>
        /// Log on post
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        [ValidateAntiForgeryToken]
        public object Login(LogOnViewModel model)
        {
            using (var unitOfWork = UnitOfWorkManager.NewUnitOfWork())
            {
                var username = model.Email;
                var password = model.Password;
                var message = new GenericMessageViewModel
                {
                    MessageType = GenericMessages.error,
                    Message = LocalizationService.GetResourceString("Errors.GenericMessage"),
                };

                try
                {
                    // We have an event here to help with Single Sign On
                    // You can do manual lookups to check users based on a web service and validate a user
                    // Then log them in if they exist or create them and log them in - Have passed in a UnitOfWork
                    // To allow database changes.

                    var e = new LoginEventArgs
                    {
                        UserName = model.Email,
                        Password = model.Password,
                        RememberMe = model.RememberMe,
                        ReturnUrl = model.ReturnUrl,
                        UnitOfWork = unitOfWork
                    };
                    EventManager.Instance.FireBeforeLogin(this, e);

                    if (!e.Cancel)
                    {
                        var user = new MembershipUser();
                        if (MembershipService.ValidateUser(username, password, System.Web.Security.Membership.MaxInvalidPasswordAttempts))
                        {
                            // Set last login date
                            user = MembershipService.GetUser(username);
                            if (user.IsApproved && !user.IsLockedOut && !user.IsBanned)
                            {
                                FormsAuthentication.SetAuthCookie(username, model.RememberMe);
                                user.LastLoginDate = DateTime.UtcNow;

                                message.Message = LocalizationService.GetResourceString("Members.NowLoggedIn");
                                message.MessageType = GenericMessages.success;

                                EventManager.Instance.FireAfterLogin(this, new LoginEventArgs
                                {
                                    UserName = model.Email,
                                    Password = model.Password,
                                    RememberMe = model.RememberMe,
                                    ReturnUrl = model.ReturnUrl,
                                    UnitOfWork = unitOfWork
                                });
                            }
                            //else if (!user.IsApproved && SettingsService.GetSettings().ManuallyAuthoriseNewMembers)
                            //{
                            //    message.Message = LocalizationService.GetResourceString("Members.NowRegisteredNeedApproval");
                            //    message.MessageType = GenericMessages.success;
                            //}
                            //else if (!user.IsApproved && SettingsService.GetSettings().NewMemberEmailConfirmation == true)
                            //{
                            //    message.Message = LocalizationService.GetResourceString("Members.MemberEmailAuthorisationNeeded");
                            //    message.MessageType = GenericMessages.success;
                            //}
                        }

                        // Only show if we have something to actually show to the user
                        if (!string.IsNullOrEmpty(message.Message))
                        {
                            return message;
                        }
                        else
                        {
                            // get here Login failed, check the login status
                            var loginStatus = MembershipService.LastLoginStatus;

                            switch (loginStatus)
                            {
                                case LoginAttemptStatus.UserNotFound:
                                case LoginAttemptStatus.PasswordIncorrect:
                                    message.Message = LocalizationService.GetResourceString("Members.Errors.PasswordIncorrect");
                                    break;

                                case LoginAttemptStatus.PasswordAttemptsExceeded:
                                    message.Message = LocalizationService.GetResourceString("Members.Errors.PasswordAttemptsExceeded");
                                    break;

                                case LoginAttemptStatus.UserLockedOut:
                                    message.Message = LocalizationService.GetResourceString("Members.Errors.UserLockedOut");
                                    break;

                                case LoginAttemptStatus.Banned:
                                    message.Message = LocalizationService.GetResourceString("Members.NowBanned");
                                    break;

                                case LoginAttemptStatus.UserNotApproved:
                                    message.Message = LocalizationService.GetResourceString("Members.Errors.UserNotApproved");
                                    user = MembershipService.GetUser(username);
                                    SendEmailConfirmationEmail(user);
                                    break;

                                default:
                                    message.Message = LocalizationService.GetResourceString("Members.Errors.LogonGeneric");
                                    break;
                            }
                        }
                    }
                }
                finally
                {
                    try
                    {
                        unitOfWork.Commit();
                    }
                    catch (Exception ex)
                    {
                        unitOfWork.Rollback();
                        LoggingService.Error(ex);
                    }
                }

                return message;
            }
        }

    }
}