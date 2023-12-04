using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyLeaveManagement.Contracts;
using MyLeaveManagement.Data;
using MyLeaveManagement.Models;
using MyLeaveManagement.Repository;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Diagnostics;

namespace MyLeaveManagement.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LeaveAllocationController : Controller
    {
        private readonly IMapper _mapper;
        /* private readonly ILeaveTypeRepository _LeaveTypeRepository;
         private readonly ILeaveAllocationRepository _leaveAllocationRepository;*/
        private readonly UserManager<Employee> _userManager;
        private readonly IUnitOfWork unitOfWork;
        public LeaveAllocationController(IMapper mapper, /*ILeaveAllocationRepository leaveAllocationRepository,
            ILeaveTypeRepository leaveTypeRepository,*/ UserManager<Employee> userManager, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            /*_leaveAllocationRepository = leaveAllocationRepository;
            _LeaveTypeRepository = leaveTypeRepository;*/
            _userManager = userManager;
            this.unitOfWork = unitOfWork;
        }

        public async Task<ActionResult> SetLeave(int id)
        {
            /*var leavetype = await _LeaveTypeRepository.findByIDAsync(id);*/
            var leavetype = await unitOfWork.LeaveTypes.GetAsync(q => q.Id == id);
            var emloyees = _userManager.GetUsersInRoleAsync("Employee");
            var period = DateTime.Now.Year;

            foreach (var e in await emloyees)
            {
                /*if (await _leaveAllocationRepository.CheckAllocationAsync(id, e.Id))*/
                if (await unitOfWork.LeaveAllocations.isExistsAsync(q => q.EmployeeId == e.Id
                                        && q.LeaveTypeId == id
                                        && q.Period == period))
                    continue;
                var allocation = new LeaveAllocationViewModel
                {
                    DateCreated = DateTime.Now,
                    EmployeeId = e.Id,
                    LeaveTypeId = id,
                    NumberOfDays = leavetype.DefaultDays,
                    Period = DateTime.Now.Year
                };
                var LeaveAllocation = _mapper.Map<LeaveAllocation>(allocation);
                await unitOfWork.LeaveAllocations.CreateAsync(LeaveAllocation);
                /*await _leaveAllocationRepository.CreateAsync(LeaveAllocation);*/
            }
            return RedirectToAction(nameof(Index));

        }
        public async Task<ActionResult> ListEmployees()
        {
            var emloyees = await _userManager.GetUsersInRoleAsync("Employee");
            var model = _mapper.Map<List<EmployeeViewModel>>(emloyees);
            return View(model);
        }

        public async Task<ActionResult> Details(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var period = DateTime.Now.Year;
            var emloyee = _mapper.Map<EmployeeViewModel>(user);
            /*var allocation = await _leaveAllocationRepository.GetLeaveAllocationsByEmloyeeAsync(id);*/
            var allocation = await unitOfWork.LeaveAllocations.GetAllAsync(
                expression: q => q.EmployeeId == id && q.Period == period);
            var allocations = _mapper.Map<List<LeaveAllocationViewModel>>(allocation);
            var model = new ViewAllocationsViewModel
            {
                employeeViewModel = emloyee,
                EmployeeId = id,
                leaveAllocationViewModel = allocations
            };
            return View(model);
        }
        public async Task<IActionResult> Index()
        {
            /* var leavetypes = await _LeaveTypeRepository.GetAllAsync();*/
            var leavetypes = await unitOfWork.LeaveTypes.GetAllAsync();
            var MappedLeaveTypes = _mapper.Map<List<LeaveType>, List<LeaveTypeViewModel>>(leavetypes.ToList());
            var model = new CreateLeaveAllocationViewModel { LeaveTypes = MappedLeaveTypes, NumberUpdated = 0 };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        // GET: LeaveAllocationController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            /*var leaveallocation = await _leaveAllocationRepository.findByIDAsync(id);*/
            var leaveallocation = await unitOfWork.LeaveAllocations.GetAsync(q => q.id == id);
            var model = _mapper.Map<EditLeaveAllocationViewModel>(leaveallocation);
            return View(model);
        }

        // POST: LeaveAllocationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditLeaveAllocationViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return View(model);
                }
                /*var Record = await _leaveAllocationRepository.findByIDAsync(model.id);*/
                var Record = await unitOfWork.LeaveAllocations.GetAsync(q => q.id == model.id);
                Record.NumberOfDays = model.NumberOfDays;
                /// var allocation = _mapper.Map<LeaveAllocation>(model);
                /*var isSuccess = await _leaveAllocationRepository.updateAsync(Record);*/
                unitOfWork.LeaveAllocations.update(Record);
                await unitOfWork.saveAsync();
                /*if (!isSuccess)
                {
                    ModelState.AddModelError("", "Error while editing");
                }*/
                return RedirectToAction(nameof(Details), new { id = model.EmployeeId });
            }
            catch
            {
                return View();
            }

        }
    }

}