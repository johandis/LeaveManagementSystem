using LeaveManagementSystem.Web.Data;

namespace LeaveManagementSystem.Web.Services.Periods
{
    public interface IPeriodsService
    {
        Task<Period> GetCurrentPeriod();
    }
}
