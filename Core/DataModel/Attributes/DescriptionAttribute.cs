using System;
using System.Web.Mvc;
using SportsRUsApp.Core.Interfaces.Services;

namespace SportsRUsApp.Core.DataModel.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DescriptionAttribute : Attribute
    {
        private readonly ILocalizationService _localizationService;
        public string Description { get; set; }

        public DescriptionAttribute(string desc)
        {
            if (_localizationService == null)
            {
                _localizationService = DependencyResolver.Current.GetService<ILocalizationService>();
            }

            Description = _localizationService.GetResourceString(desc.Trim());
        }
    }
}
