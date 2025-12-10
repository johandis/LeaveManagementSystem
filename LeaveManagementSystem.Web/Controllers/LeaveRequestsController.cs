using LeaveManagementSystem.Web.Models.LeaveRequests;
using LeaveManagementSystem.Web.Services.LeaveRequests;
using LeaveManagementSystem.Web.Services.LeaveTypes;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LeaveManagementSystem.Web.Controllers
{
    [Authorize]
    public class LeaveRequestsController (ILeaveTypesService leaveTypesService, ILeaveRequestsService leaveRequestsService) : Controller
    {
        // Employee View requests
        public async Task<IActionResult> Index()
        {
            var model = await leaveRequestsService.GetEmployeeLeaveRequests();
            return View(model);
        }

        // Employee Create requests
        public async Task<IActionResult> Create(int? leaveTypeId)
        {
            var leaveTypes = await leaveTypesService.GetAll();
            var leaveTypeList = new SelectList(leaveTypes, "Id", "Name", leaveTypeId);
            var model = new LeaveRequestCreateVM
            {
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                EndDate = DateOnly.FromDateTime(DateTime.Now).AddDays(1),
                LeaveTypes = leaveTypeList
            };

            return View(model);
        }

        // Employee Create requests
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveRequestCreateVM model)
        {
            // Validate that the days don't exceed the allocation
            if (await leaveRequestsService.RequestDatesExceedAllocation(model))
            {
                ModelState.AddModelError(string.Empty, "You have exceeded your allocation");
                ModelState.AddModelError(nameof(model.EndDate), "The number of days requested is invalid. ");
            }

            if (ModelState.IsValid)
            {
                await leaveRequestsService.CreateLeaveRequest(model);
                return RedirectToAction(nameof(Index));
            }

            var leaveTypes = await leaveTypesService.GetAll();
            model.LeaveTypes = new SelectList(leaveTypes, "Id", "Name");
            return View(model);
        }

        // Employee Cancel requests
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id)
        {
            await leaveRequestsService.CancelLeaveRequest(id);
            return RedirectToAction(nameof(Index));
        }

        // Admin/Supe review requests
        [Authorize(Policy = "AdminSupervisorOnly")]
        public async Task<IActionResult> ListRequests()
        {
            var model = await leaveRequestsService.AdminGetAllLeaveRequests();
            return View(model);
        }

        // Admin/Supe review requests
        public async Task<IActionResult> Review(int id)
        {
            var model = await leaveRequestsService.GetLeaveRequestForReview(id);
            return View(model);
        }

        // Admin/Supe review requests
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Review(int id, bool approved)
        {
            await leaveRequestsService.ReviewLeaveRequest(id, approved);
            return RedirectToAction(nameof(ListRequests));
        }
    }
}