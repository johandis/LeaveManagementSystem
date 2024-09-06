using AutoMapper;
using LeaveManagementSystem.Web.Data;
using LeaveManagementSystem.Web.Models.LeaveTypes;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Services
{
    public class LeaveTypesService(ApplicationDbContext context, IMapper mapper) : ILeaveTypesService
    {
        public async Task<List<LeaveTypeReadOnlyVM>> GetAll()
        {

            // var data =  SELECT * FROM LeaveTypes 
            var data = await context.LeaveTypes.ToListAsync();

            // Convert data model into a view model - use autoMapper
            var viewData = mapper.Map<List<LeaveTypeReadOnlyVM>>(data);
            return viewData;
        }

        public async Task<T?> Get<T> (int id) where T : class
        {
            var data = await context.LeaveTypes.FirstOrDefaultAsync(x => x.Id == id);
            if (data == null)
            {
                return null;
            }

            var viewData = mapper.Map<T>(data);
            return viewData;
        }

        public async Task Remove(int id)
        {
            var data = await context.LeaveTypes.FirstOrDefaultAsync(x => x.Id == id);
            if (data != null)
            {
                context.Remove (data);
                await context.SaveChangesAsync();
            }
        }

        public async Task Edit(LeaveTypeEditVM model)
        {
            var leaveType = mapper.Map<LeaveType>(model);
            context.Update(leaveType);
            await context.SaveChangesAsync();
        }

        public async Task Create(LeaveTypeCreateVM model)
        {

            var leaveType = mapper.Map<LeaveType>(model);
            context.Add(leaveType);
            await context.SaveChangesAsync();
        }

        public bool LeaveTypeExists(int id)
        {
            return context.LeaveTypes.Any(e => e.Id == id);
        }

        public async Task<bool> CheckIfLeaveTypeNameExist(string name)
        {
            var lowerCaseName = name.ToLower();
            return await context.LeaveTypes.AnyAsync(q => q.Name.ToLower().Equals(lowerCaseName));
        }

        public async Task<bool> CheckIfLeaveTypeNameExistForEdit(LeaveTypeEditVM leaveTypeEdit)
        {
            var lowerCaseName = leaveTypeEdit.Name.ToLower();
            return await context.LeaveTypes.AnyAsync(q => q.Name.ToLower().Equals(lowerCaseName) &&
                                                           q.Id != leaveTypeEdit.Id);
        }
    }
}
