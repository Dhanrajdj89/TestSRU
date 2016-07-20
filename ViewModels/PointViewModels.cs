using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportsRUsApp.Core.DataModel;

namespace SportsRUsApp.Web.ViewModels
{
    public class HighEarnersPointViewModel
    {
        public Dictionary<MembershipUser, int> HighEarners { get; set; }
    }
}