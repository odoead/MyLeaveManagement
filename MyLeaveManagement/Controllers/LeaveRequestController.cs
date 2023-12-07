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

        public LeaveRequestController(ILeaveRequestRepository leaveRequestRepository,
            IMapper mapper, UserManager<Employee> userManager, ILeaveTypeRepository leaveTypeRepository, ILeaveAllocationRepository leaveAllocation)
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
                ApprovedRequests = leaveRequestsModel.Where(q => q.Approved == true).Count(),
                PendingRequests = leaveRequestsModel.Where(q => q.Approved == null).Count(),
                RejectedRequests = leaveRequestsModel.Where(q => q.Approved == false).Count(),
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
                    GetLeaveAllocationsByEmloyeeAndTypeAsync(leaveRequest.RequestingEmpoyeeId, leaveRequest.Id);
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
        public async Task<ActionResult> CancelRequest(int id)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var leaveRequest = await _leaveRequestRepository.FindByIdAsync(id);
                leaveRequest.IsApproved = false;
                leaveRequest.ApprovedById = user.Id;
                leaveRequest.DateRequested = DateTime.Now;
                var isSuccess = await _leaveRequestRepository.UpdateAsync(leaveRequest);

                return RedirectToAction(nameof(Index));

            }
            catch (Exception)
            {

                return RedirectToAction(nameof(Index));
            }

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



            /*var leaveTypes = _leaveTypeRepository.GetAll();
            var leaveTItems = leaveTypes.Select(q => new System.Web.Mvc.SelectListItem
            { Text = q.Name, Value = q.Id.ToString()});
            var model = new CreateLeaveRequestViewModel
            {
                LeaveTypes = leaveTItems
            };
            return View(model);*/

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

                var leaveTItems = leaveTypes.Select(q => new SelectListItem
                {
                    Text = q.Name,
                    Value = q.Id.ToString()
                });
                model.LeaveTypes = leaveTItems;



                //Console.WriteLine(model.LeaveTypes.);
                /*if (ModelState.IsValid==false)
                {
                    return View(model);
                }*/
                if (DateTime.Compare(startDate, endDate) > 1)
                {
                    ModelState.AddModelError("", "start date is bigger than end date");
                    return View(model);
                }
                var employee = await _userManager.GetUserAsync(User);

                var allocation = await _leaveAllocationRepository
                    .GetLeaveAllocationsByEmloyeeAndTypeAsync(employee.Id, model.LeaveTypeId);

                int daysRequsted =
                    (int)(endDate.Date - startDate.Date).TotalDays;

                if (daysRequsted > allocation.NumberOfDays)
                {
                    ModelState.AddModelError("", "not enought vacation dates");
                    return View(model);
                }
                var leaveRequestVM = new LeaveRequestViewModel
                {
                    RequestingEmployeeId = employee.Id,
                    StartDate = startDate,
                    EndDate = endDate,
                    Approved = null,
                    DateRequested = DateTime.Now,
                    DateActioned = DateTime.Now,
                    LeaveTypeId = model.LeaveTypeId,

                };
                var leaveRequest = _mapper.Map<LeaveRequest>(leaveRequestVM);
                var isSuccess = await _leaveRequestRepository.CreateAsync(leaveRequest);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "error while creating leave request in repo");
                }
                return RedirectToAction("MyLeave");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "error");
                return View(model);
            }
        }

        // GET: LeaveRequestController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LeaveRequestController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // GET: LeaveRequestController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeaveRequestController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
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
