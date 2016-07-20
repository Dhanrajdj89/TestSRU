using System.Collections.Generic;
using SportsRUsApp.Core.DataModel;

namespace SportsRUsApp.Web.Areas.Admin.ViewModels
{
    public class ListLogViewModel
    {
        public IList<LogEntry> LogFiles { get; set; }
    }
}