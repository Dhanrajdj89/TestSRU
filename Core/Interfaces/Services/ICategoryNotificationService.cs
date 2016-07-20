﻿using System;
using System.Collections.Generic;
using SportsRUsApp.Core.DataModel;

namespace SportsRUsApp.Core.Interfaces.Services
{
    public partial interface ICategoryNotificationService
    {
        IList<CategoryNotification> GetAll();
        void Delete(CategoryNotification notification);
        IList<CategoryNotification> GetByCategory(Category category);
        IList<CategoryNotification> GetByUser(MembershipUser user);
        IList<CategoryNotification> GetByUserAndCategory(MembershipUser user, Category category, bool addTracking = false);
        CategoryNotification Add(CategoryNotification category);
        CategoryNotification Get(Guid id);
    }
}
