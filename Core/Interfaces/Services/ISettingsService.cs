using System;
using SportsRUsApp.Core.DataModel;

namespace SportsRUsApp.Core.Interfaces.Services
{
    public partial interface ISettingsService
    {
        Settings GetSettings(bool useCache = true);
        Settings Add(Settings settings);
        Settings Get(Guid id);
    }
}
