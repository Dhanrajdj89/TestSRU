using System;
using System.Collections.Generic;
using SportsRUsApp.Core.DataModel;

namespace SportsRUsApp.Web.ViewModels
{
    public class LanguageListAllViewModel
    {
        public IEnumerable<Language> Alllanguages { get; set; }
        public Guid CurrentLanguage { get; set; }
    }
}