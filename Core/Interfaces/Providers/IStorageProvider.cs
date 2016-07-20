using System.Web;

namespace SportsRUsApp.Core.Interfaces.Providers
{
    public interface IStorageProvider
    {
        string GetUploadFolderPath(bool createIfNotExist, params object[] subFolders);

        string BuildFileUrl(params object[] subPath);

        string SaveAs(string uploadFolderPath, string fileName, HttpPostedFileBase file);
    }
}