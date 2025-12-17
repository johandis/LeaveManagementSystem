using AutoMapper;
using LeaveManagementSystem.Application.Models.LeaveTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LeaveManagementSystem.Application.Services.LeaveTypes
{
    public class LeaveTypesService(ApplicationDbContext context, IMapper mapper, 
        ILogger<LeaveTypesService> logger) : ILeaveTypesService
    {
        public async Task<List<LeaveTypeReadOnlyVM>> GetAll()
        {

            // var data =  SELECT * FROM LeaveTypes 
            var data = await context.LeaveTypes.ToListAsync();

            // Convert data model into a view model - use autoMapper
            var viewData = mapper.Map<List<LeaveTypeReadOnlyVM>>(data);
            return viewData;
        }

        public async Task<T?> Get<T>(int id) where T : class
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
                context.Remove(data);
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
            logger.LogInformation("Creating Leave Type: {leaveTypeName} - {days}", model.Name, 
                model.NumberOfDays);
            var leaveType = mapper.Map<LeaveType>(model);
            context.Add(leaveType);
            await context.SaveChangesAsync();
        }

        public bool LeaveTypeExists(int id)
        {
            return context.LeaveTypes.Any(e => e.Id == id);
        }

        public async Task<bool> CheckIfLeaveTypeNameExists(string name)
        {
            var lowerCaseName = name.ToLower();
            return await context.LeaveTypes.AnyAsync(q => q.Name.ToLower().Equals(lowerCaseName));
        }

        public async Task<bool> CheckIfLeaveTypeNameExistsForEdit(LeaveTypeEditVM leaveTypeEdit)
        {
            var lowerCaseName = leaveTypeEdit.Name.ToLower();
            return await context.LeaveTypes.AnyAsync(q => q.Name.ToLower().Equals(lowerCaseName) &&
                                                           q.Id != leaveTypeEdit.Id);
        }

        public async Task<bool> DaysExceedMaximum(int leaveTypeId, int days)
        {
            var leaveType = await context.LeaveTypes.FindAsync(leaveTypeId);
            return leaveType.NumberOfDays < days;
        }
    }
}
