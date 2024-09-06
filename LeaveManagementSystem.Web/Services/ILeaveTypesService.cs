using LeaveManagementSystem.Web.Models.LeaveTypes;

namespace LeaveManagementSystem.Web.Services;

public interface ILeaveTypesService
{
    Task<List<LeaveTypeReadOnlyVM>> GetAll();
    Task<T?> Get<T> (int id) where T : class;
    Task Remove(int id);
    Task Edit(LeaveTypeEditVM model);
    Task Create(LeaveTypeCreateVM model);
    bool LeaveTypeExists(int id);
    Task<bool> CheckIfLeaveTypeNameExist(string name);
    Task<bool> CheckIfLeaveTypeNameExistForEdit(LeaveTypeEditVM leaveTypeEdit);
}