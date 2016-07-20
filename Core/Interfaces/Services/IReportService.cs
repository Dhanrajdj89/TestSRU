using SportsRUsApp.Core.DataModel;

namespace SportsRUsApp.Core.Interfaces.Services
{
    public partial interface IReportService
    {
        void MemberReport(Report report);
        void PostReport(Report report);
    }
}
