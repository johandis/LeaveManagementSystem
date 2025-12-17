using Microsoft.AspNetCore.Http;

namespace LeaveManagementSystem.Application.Services.Users
{
    public class UserService(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor) : IUserService
    {
        public async Task<ApplicationUser> GetLoggedInUser()
        {
            var user = await userManager.GetUserAsync(httpContextAccessor.HttpContext?.User);
            return user;
        }

        public async Task<ApplicationUser> GetUserById(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            return user;
        }

        public async Task<List<ApplicationUser>> GetEmployees()
        {
            var employees = await userManager.GetUsersInRoleAsync(Roles.Employee);
            return employees.ToList();
        }
    }
}
