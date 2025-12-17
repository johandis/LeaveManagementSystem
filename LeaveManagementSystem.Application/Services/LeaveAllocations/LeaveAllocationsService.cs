using AutoMapper;
using LeaveManagementSystem.Application.Models.LeaveAllocations;
using LeaveManagementSystem.Application.Services.Periods;
using LeaveManagementSystem.Application.Services.Users;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Application.Services.LeaveAllocations
{
    public class LeaveAllocationsService(ApplicationDbContext context, IUserService userService, IMapper mapper, IPeriodsService periodsService) : ILeaveAllocationsService
    {
        public async Task AllocateLeave(string employeeId)
        {
            // get all the leave types
            var leaveTypes = await context.LeaveTypes
                .Where(q => !q.LeaveAllocations.Any(x => x.EmployeeId == employeeId)).ToListAsync();

            // get the current period based on the year
            var period = await periodsService.GetCurrentPeriod();
            var monthsRemaining = period.EndDate.Month - DateTime.Now.Month;

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

                var leaveAllocation = new LeaveAllocation
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
                ? await userService.GetLoggedInUser()
                : await userService.GetUserById(userId);

            var allocations = await GetAllocations(user.Id);
            var allocationVMList = mapper.Map<List<LeaveAllocation>, List<LeaveAllocationVM>>(allocations);
            var leaveTypesCount = await context.LeaveTypes.CountAsync();

            var employeeVM = new EmployeeAllocationVM
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
            var users = await userService.GetEmployees();
            var employees = mapper.Map<List<ApplicationUser>, List<EmployeeListVM>>(users.ToList());

            return employees;
        }

        public async Task<LeaveAllocationEditVM> GetEmployeeAllocation(int allocationId)
        {
            var allocation = await context.LeaveAllocations
                .Include(q => q.LeaveType)
                .Include(q => q.Employee)
                .FirstOrDefaultAsync(q => q.Id == allocationId);

            var model = mapper.Map<LeaveAllocationEditVM>(allocation);

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

        public async Task<LeaveAllocation> GetCurrentAllocation(int leaveTypeId, string employeeId)
        {
            var period = await periodsService.GetCurrentPeriod();
            var allocation = await context.LeaveAllocations
                .FirstAsync(q => q.LeaveTypeId == leaveTypeId
                                 && q.EmployeeId == employeeId
                                 && q.PeriodId == period.Id);
            return allocation;
        }

        private async Task<List<LeaveAllocation>> GetAllocations(string? userId)
        {
            var period = await periodsService.GetCurrentPeriod();
            var leaveAllocations = await context.LeaveAllocations
                .Include(q => q.LeaveType)
                .Include(q => q.Period)
                .Where(q => q.EmployeeId == userId && q.Period.Id == period.Id)
                .ToListAsync();
            return leaveAllocations;
        }

        private async Task<bool> AllocationExists(string userId, int periodId, int leaveTypeId)
        {
            var exist = await context.LeaveAllocations.AnyAsync(q =>
                q.EmployeeId == userId && q.PeriodId == periodId && q.LeaveTypeId == leaveTypeId);

            return exist;
        }
    }
}
