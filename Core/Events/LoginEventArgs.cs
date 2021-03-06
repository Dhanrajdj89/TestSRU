﻿using SportsRUsApp.Core.Interfaces.UnitOfWork;

namespace SportsRUsApp.Core.Events
{
    public class LoginEventArgs :  MVCForumEventArgs
    {
        public string ReturnUrl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }
    }
}
