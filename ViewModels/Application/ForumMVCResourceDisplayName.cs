using System.ComponentModel;
using SportsRUsApp.Core.Interfaces;
using SportsRUsApp.Core.Interfaces.Services;

namespace SportsRUsApp.ViewModels.Application
{
    public class ForumMvcResourceDisplayName: DisplayNameAttribute, IModelAttribute
    {
            private string _resourceValue = string.Empty;
            private readonly ILocalizationService _localizationService;

            public ForumMvcResourceDisplayName(string resourceKey)
                : base(resourceKey)
            {
                ResourceKey = resourceKey;
                _localizationService = ServiceFactory.Get<ILocalizationService>();
            }

            public string ResourceKey { get; set; }

            public override string DisplayName
            {
                get
                {
                    _resourceValue = _localizationService.GetResourceString(ResourceKey.Trim());
                    return _resourceValue;
                }
            }

            public string Name
            {
                get { return "FMVCResourceDisplayName"; }
            }

    }
}

