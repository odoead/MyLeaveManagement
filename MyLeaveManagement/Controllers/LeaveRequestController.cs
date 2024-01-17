using AutoMapper;
using MyLeaveManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyLeaveManagement.Contracts;
using MyLeaveManagement.Data;
using MyLeaveManagement.Models;
using MyLeaveManagement.Repository;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Web.WebPages;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace MyLeaveManagement.Controllers
{
    [Authorize]
    public class LeaveRequestController : Controller
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;

        private readonly IMapper _mapper;
        private readonly UserManager<Employee> _userManager;

        public LeaveRequestController(IMapper mapper, UserManager<Employee> userManager,
            ILeaveRequestRepository leaveRequestRepository, ILeaveTypeRepository leaveTypeRepository,
            ILeaveAllocationRepository leaveAllocation)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
            _userManager = userManager;
            _leaveTypeRepository = leaveTypeRepository;
            _leaveAllocationRepository = leaveAllocation;
        }
        [Authorize(Roles = "Admin")]
        // GET: LeaveRequestController
        public async Task<ActionResult> Index()
        {
            var requests = await _leaveRequestRepository.GetAllAsync();
            var leaveRequestsModel = _mapper.Map<List<LeaveRequestViewModel>>(requests);
            var model = new AdminLeaveRequestViewViewModel
            {
                TotalRequests = leaveRequestsModel.Count,
                ApprovedRequests = leaveRequestsModel.Count(q => q.Approved == true),
                PendingRequests = leaveRequestsModel.Count(q => q.Approved == null),
                RejectedRequests = leaveRequestsModel.Count(q => q.Approved == false),
                LeaveRequests = leaveRequestsModel
            };




            return View(model);
        }

        // GET: LeaveRequestController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var leaveRequest = await _leaveAllocationRepository.FindByIdAsync(id);
            var model = _mapper.Map<LeaveRequestViewModel>(leaveRequest);
            return View(model);
        }
        public async Task<ActionResult> ApproveRequest(int id)
        {
            try
            {
                var user = _userManager.GetUserAsync(User).Result;
                var leaveRequest = await _leaveRequestRepository.FindByIdAsync(id);
                var allocation = await _leaveAllocationRepository.
                    GetLeaveAllocationByEmloyeeAndTypeAsync(leaveRequest.RequestingEmpoyeeId, leaveRequest.Id);
                var leaveTypeId = leaveRequest.LeaveTypeId;
                leaveRequest.IsApproved = true;
                leaveRequest.ApprovedById = user.Id;
                leaveRequest.DateRequested = DateTime.Now;
                int daysRequsted = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;
                allocation.NumberOfDays -= daysRequsted;
                await _leaveRequestRepository.UpdateAsync(leaveRequest);
                await _leaveAllocationRepository.UpdateAsync(allocation);

                return RedirectToAction(nameof(Index));

            }
            catch (Exception)
            {

                return RedirectToAction(nameof(Index));
            }

        }
        public async Task<ActionResult> RejectRequest(int id)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var leaveRequest = await _leaveRequestRepository.FindByIdAsync(id);
                leaveRequest.IsApproved = false;
                leaveRequest.ApprovedById = user.Id;
                leaveRequest.DateRequested = DateTime.Now;
                await _leaveRequestRepository.UpdateAsync(leaveRequest);

                return RedirectToAction(nameof(Index));

            }
            catch (Exception)
            {

                return RedirectToAction(nameof(Index));
            }

        }

        public async Task<ActionResult> CancelRequest(int id)
        {
            var leaveRequest = await _leaveRequestRepository.FindByIdAsync(id);
            leaveRequest.IsApproved = false;
            _leaveRequestRepository.UpdateAsync(leaveRequest);
            await _leaveRequestRepository.SaveAsync();

            return RedirectToAction("MyLeave");
        }
        // GET: LeaveRequestController/Create
        public async Task<ActionResult> Create()
        {

            var leaveTypes = await _leaveTypeRepository.GetAllAsync();

            var leaveTItems = leaveTypes.Select(q => new SelectListItem
            {
                Text = q.Name,
                Value = q.Id.ToString()
            });
            var model = new CreateLeaveRequestViewModel
            {
                LeaveTypes = leaveTItems
            };
            return View(model);



        }

        // POST: LeaveRequestController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateLeaveRequestViewModel model)//ne rabotaet
        {
            try
            {

                var startDate = DateTime.ParseExact(model.StartDate, "MM/dd/yyyy", null);
                var endDate = DateTime.ParseExact(model.EndDate, "MM/dd/yyyy", null);
                var leaveTypes = await _leaveTypeRepository.GetAllAsync();
                var employee = await _userManager.GetUserAsync(User);
                var allocation = await _leaveAllocationRepository
                    .GetLeaveAllocationByEmloyeeAndTypeAsync(employee.Id, model.LeaveTypeId);
                int daysRequested = (int)(endDate.Date - startDate.Date).TotalDays;
                var leaveTItems = leaveTypes.Select(q => new SelectListItem
                {
                    Text = q.Name,
                    Value = q.Id.ToString()
                });
                model.LeaveTypes = leaveTItems;


                if (allocation == null)
                {
                    ModelState.AddModelError("", "You Have No Days Left");
                }
                if (DateTime.Compare(startDate, endDate) > 1)
                {
                    ModelState.AddModelError("", "Start Date cannot be further in the future than the End Date");
                }
                if (daysRequested > allocation.NumberOfDays)
                {
                    ModelState.AddModelError("", "You Do Not Sufficient Days For This Request");
                }
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var leaveRequestModel = new LeaveRequestViewModel
                {
                    RequestingEmployeeId = employee.Id,
                    StartDate = startDate,
                    EndDate = endDate,
                    Approved = null,
                    DateRequested = DateTime.Now,
                    DateActioned = DateTime.Now,
                    LeaveTypeId = model.LeaveTypeId,
                    RequestComments = model.RequestComments
                };

                var leaveRequest = _mapper.Map<LeaveRequest>(leaveRequestModel);
                var isSuccess = await _leaveRequestRepository.CreateAsync(leaveRequest);

                return RedirectToAction("MyLeave");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Something went wrong");
                return View(model);
            }
        }

        public async Task<ActionResult> MyLeave()
        {
            var employee = _userManager.GetUserAsync(User).Result;
            var employeeId = employee.Id;
            var employeeAllocations = await _leaveAllocationRepository.GetLeaveAllocationsByEmloyeeAsync(employeeId);
            var employeeRequests = await _leaveRequestRepository.GetRequestsByEmployeeAsync(employeeId);
            var employeeAllocationModel = _mapper.Map<List<LeaveAllocationViewModel>>(employeeAllocations);
            var employeeRequestModel = _mapper.Map<List<LeaveRequestViewModel>>(employeeRequests);
            var model = new EmployeeLeaveRequestViewViewModel
            {
                LeaveAllocations = employeeAllocationModel,
                LeaveRequests = employeeRequestModel
            };
            return View(model);
        }
    }
}
