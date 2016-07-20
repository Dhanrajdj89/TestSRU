using System;
using SportsRUsApp.Core.Constants;
using SportsRUsApp.Core.Interfaces.Providers;

namespace SportsRUsApp.Web.Application.StorageProviders
{
    public static class StorageProvider
    {
        private static readonly Lazy<IStorageProvider> CurrentStorageProvider = new Lazy<IStorageProvider>(() =>
        {
            var type = SiteConstants.Instance.StorageProviderType;
            if (string.IsNullOrEmpty(type))
            {
                return new DiskStorageProvider();
            }

            try
            {
                return TypeFactory.GetInstanceOf<IStorageProvider>(type);
            }
            catch (Exception)
            {
                return new DiskStorageProvider();
            }
        });

        public static IStorageProvider Current => CurrentStorageProvider.Value;
    }
}