using SportsRUsApp.Core.Constants;
using SportsRUsApp.Core.DataModel;
using SportsRUsApp.Core.Interfaces.Services;
using SportsRUsApp.ViewModels.Application;
using SportsRUsApp.Web.Application;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;

namespace SportsRUsApp.Web.Controllers.ApiControllers
{
    [Authorize]
    [RoutePrefix("api/TinyMce")]
    public class TinyMcePluginsController : BaseApiController
    {
        private IUploadedFileService uploadService { get; set; } = ServiceFactory.Get<IUploadedFileService>();

        //private void SetPrincipal(IPrincipal principal)
        //{
        //    Thread.CurrentPrincipal = principal;
        //    if (HttpContext.Current != null)
        //    {
        //        HttpContext.Current.User = principal;
        //    }
        //}

        //GET api/TinyMce/UploadImage
        [Route("UploadImage")]
        [HttpPost]
        public string UploadImage()
        {
            using (var unitOfWork = UnitOfWorkManager.NewUnitOfWork())
            {
                try
                {
                    if (HttpContext.Current.Request.Files.AllKeys.Any())
                    {
                        // Get the uploaded image from the Files collection
                        var httpPostedFile = HttpContext.Current.Request.Files["file"];
                        if (httpPostedFile != null)
                        {
                            HttpPostedFileBase photo = new HttpPostedFileWrapper(httpPostedFile);
                            var loggedOnReadOnlyUser = MembershipService.GetUser(HttpContext.Current.User.Identity.Name);
                            var permissions = RoleService.GetPermissions(null, loggedOnReadOnlyUser.Roles.FirstOrDefault());
                            // Get the permissions for this category, and check they are allowed to update
                            if (permissions[SiteConstants.Instance.PermissionInsertEditorImages].IsTicked && loggedOnReadOnlyUser.DisableFileUploads != true)
                            {
                                // woot! User has permission and all seems ok
                                // Before we save anything, check the user already has an upload folder and if not create one
                                var uploadFolderPath = HostingEnvironment.MapPath(string.Concat(SiteConstants.Instance.UploadFolderPath, loggedOnReadOnlyUser.Id));
                                if (!Directory.Exists(uploadFolderPath))
                                {
                                    Directory.CreateDirectory(uploadFolderPath);
                                }

                                // If successful then upload the file
                                var uploadResult = AppHelpers.UploadFile(photo, uploadFolderPath, LocalizationService, true);
                                if (!uploadResult.UploadSuccessful)
                                {
                                    return string.Empty;
                                }

                                // Add the filename to the database
                                var uploadedFile = new UploadedFile
                                {
                                    Filename = uploadResult.UploadedFileName,
                                    MembershipUser = loggedOnReadOnlyUser
                                };
                                uploadService.Add(uploadedFile);

                                // Commit the changes
                                unitOfWork.Commit();

                                return uploadResult.UploadedFileUrl;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    unitOfWork.Rollback();
                    LoggingService.Error(ex);
                }
            }
            return string.Empty;
        }
    }
}

