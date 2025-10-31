using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using LeaveManagementSystem.Web.Models.LeaveAllocations;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Services.LeaveAllocations
{
    public class LeaveAllocationsService (ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, 
        UserManager<ApplicationUser> userManager, IMapper _mapper) : ILeaveAllocationsService
    {
        public async Task AllocateLeave(string employeeId)
        {
            // get all the leave types
            var leaveTypes = await context.LeaveTypes
                .Where(q => !q.LeaveAllocations.Any(x => x.EmployeeId == employeeId)).ToListAsync();

            // get the current period based on the year
            var currentDate  = DateTime.Now;
            var period = await context.Periods.SingleAsync(q => q.EndDate.Year == currentDate.Year);
            var monthsRemaining = period.EndDate.Month - currentDate.Month;

            // foreach leave type create an allocation entry
            foreach (var leaveType in leaveTypes)
            {
                // Works but not best practice
                //var allocationExist = await AllocationExist(employeeId, period.Id, leaveType.Id);
                //if (allocationExist)
                //{
                //    continue;
                //}

                var accrualRate = decimal.Divide(leaveType.NumberOfDays, 12);

                var leaveAllocation = new LeaveAllocation()
                {
                    EmployeeId = employeeId,
                    LeaveTypeId = leaveType.Id,
                    PeriodId = period.Id,
                    Days = (int)Math.Ceiling(accrualRate * monthsRemaining)
                };

                context.Add(leaveAllocation);
            }

            await context.SaveChangesAsync();
        }

        public async Task<EmployeeAllocationVM> GetEmployeeAllocations(string? userId)
        {
            var user = string.IsNullOrEmpty(userId)
                ? await userManager.GetUserAsync(httpContextAccessor.HttpContext?.User)
                : await userManager.FindByIdAsync(userId);

            var allocations = await GetAllocations(user.Id);
            var allocationVMList = _mapper.Map<List<LeaveAllocation>, List<LeaveAllocationVM>>(allocations);
            var leaveTypesCount = await context.LeaveTypes.CountAsync();
            var employeeVM = new EmployeeAllocationVM()
            {
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                LeaveAllocations = allocationVMList,
                IsCompletedAllocation = leaveTypesCount == allocations.Count
            };

            return employeeVM;
        }

        public async Task<List<EmployeeListVM>> GetEmployees()
        {
            var users = await userManager.GetUsersInRoleAsync(Roles.Employee);
            var employees = _mapper.Map<List<ApplicationUser>, List<EmployeeListVM>>(users.ToList());
            
            return employees;
        }
        
        public async Task<LeaveAllocationEditVM> GetEmployeeAllocation(int allocationId)
        {
            var allocation = await context.LeaveAllocations
                .Include(q => q.LeaveType)
                .Include(q => q.Employee)
                .FirstOrDefaultAsync(q => q.Id == allocationId);

            var model = _mapper.Map<LeaveAllocationEditVM>(allocation);

            return model;
        }

        public async Task EditAllocation(LeaveAllocationEditVM allocationEditVm)
        {
            //var leaveAllocation = await GetEmployeeAllocation(allocationEditVm.Id);
            //if (leaveAllocation == null)
            //{
            //    throw new Exception("Leave allocation record does not exist");
            //}
            //leaveAllocation.Days = allocationEditVm.Days;
            // option 1 context.Update(leaveAllocation);
            // option 2 context.Entry(leaveAllocation).State = EntityState.Modified;
            // await context.SaveChangesAsync();

            await context.LeaveAllocations
                .Where(q => q.Id == allocationEditVm.Id)
                .ExecuteUpdateAsync(s => s.SetProperty(e => e.Days, allocationEditVm.Days));
        }

        private async Task<List<LeaveAllocation>> GetAllocations(string? userId)
        {
            var currentDate = DateTime.Now;

            //var period = await context.Periods.SingleAsync(q => q.EndDate.Year == currentDate.Year);
            //var leaveAllocations = await context.LeaveAllocations
            //    .Include(q => q.LeaveType)
            //    .Include(q => q.Period)
            //    .Where(q => q.EmployeeId == user.Id && q.PeriodId == period.Id)
            //    .ToListAsync();

            var leaveAllocations = await context.LeaveAllocations
                .Include(q => q.LeaveType)
                .Include(q => q.Period)
                .Where(q => q.EmployeeId == userId && q.Period.EndDate.Year == currentDate.Year)
                .ToListAsync();

            return leaveAllocations;
        }

        private async Task<bool> AllocationExist(string userId, int periodId, int leaveTypeId)
        {
            var exist = await context.LeaveAllocations.AnyAsync(q =>
                q.EmployeeId == userId && q.PeriodId == periodId && q.LeaveTypeId == leaveTypeId);

            return exist;
        }
    }
}
