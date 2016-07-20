using System;
using System.Web;
using System.Data.Entity;
using System.Linq;
using SportsRUsApp.Core.Constants;
using SportsRUsApp.Core.DataModel;
using SportsRUsApp.Core.DataModel.Enums;
using SportsRUsApp.Core.Interfaces;
using SportsRUsApp.Core.Interfaces.Services;
using SportsRUsApp.Services.Data.Context;

namespace SportsRUsApp.Services
{
    public partial class SettingsService : ISettingsService
    {
        private readonly SportsRUsContext _context;
        private readonly ICacheService _cacheService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"> </param>
        /// <param name="cacheService"></param>
        public SettingsService(ISportsRUsContext context, ICacheService cacheService)
        {
            _cacheService = cacheService;
            _context = context as SportsRUsContext;
        }

        /// <summary>
        /// Get the site settings from cache, if not in cache gets from database and adds into the cache
        /// </summary>
        /// <returns></returns>
        public Settings GetSettings(bool useCache = true)
        {
            if (useCache)
            {
                var cachedSettings = _cacheService.Get<Settings>(AppConstants.SettingsCacheName);
                if (cachedSettings == null)
                {
                    cachedSettings = GetSettingsLocal(false);
                    if(cachedSettings == null)
                    {
                        new Migrations.Configuration().CallSeed();
                        cachedSettings = GetSettingsLocal(false);
                    }
                    _cacheService.Set(AppConstants.SettingsCacheName, cachedSettings, CacheTimes.OneDay);
                }
                return cachedSettings;
            }
            return GetSettingsLocal(true);
        }

        private Settings GetSettingsLocal(bool addTracking)
        {
            var settings = _context.Setting
                              .Include(x => x.DefaultLanguage)
                              .Include(x => x.NewMemberStartingRole);

            return addTracking ? settings.FirstOrDefault() : settings.AsNoTracking().FirstOrDefault();
        }

        public Settings Add(Settings settings)
        {
            return _context.Setting.Add(settings);
        }

        public Settings Get(Guid id)
        {
            return _context.Setting.FirstOrDefault(x => x.Id == id);
        }
    }
}
