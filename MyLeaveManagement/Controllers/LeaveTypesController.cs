﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyLeaveManagement.Contracts;
using MyLeaveManagement.Data;
using MyLeaveManagement.Models;

namespace MyLeaveManagement.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LeaveTypesController : Controller
    {
        private readonly IMapper mapper;
        private readonly ILeaveTypeRepository Repository;
        public LeaveTypesController(IMapper mapper, ILeaveTypeRepository repository)
        {
            this.mapper = mapper;
            Repository = repository;
        }
        // GET: LeaveTypesController
        public async Task<ActionResult> Index()
        {
            var q = await Repository.GetAllAsync();
            var leavetypes = q.ToList();
            var model = mapper.Map<List<LeaveType>, List<LeaveTypeViewModel>>(leavetypes);
            return View(model);
        }

        // GET: LeaveTypesController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var q = await Repository.isExistsAsync(id);
            if (!q)
            {
                return NotFound();
            }
            var leavetype = await Repository.findByIDAsync(id);
            var model = mapper.Map<LeaveTypeViewModel>(leavetype);

            return View(model);
        }

        // GET: LeaveTypesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LeaveTypeViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var leaveType = mapper.Map<LeaveType>(model);
                leaveType.DateCreated = DateTime.Now;
                var isSuccess = await Repository.CreateAsync(leaveType);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "something went wrong");
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LeaveTypesController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var q = await Repository.isExistsAsync(id);
            if (!q)
            {
                return NotFound();
            }
            var leaveType = await Repository.findByIDAsync(id);
            var model = mapper.Map<LeaveTypeViewModel>(leaveType);
            return View(model);
        }

        // POST: LeaveTypesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(LeaveTypeViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var leaveType = mapper.Map<LeaveType>(model);
                var isSuccess = await Repository.updateAsync(leaveType);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "something went wrong");
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "something went wrong");
                return View();
            }
        }

        // GET: LeaveTypesController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var type = await Repository.findByIDAsync(id);
            if (type == null)
            {
                return NotFound();
            }
            bool isSuccess = await Repository.deleteAsync(type);
            if (!isSuccess)
            {
                return BadRequest();
            }


            return RedirectToAction(nameof(Index));
            /*if (!Repository.isExists(id))
           {
               return NotFound();
           }
           var leaveType = Repository.findByID(id);
           var model = mapper.Map<LeaveTypeViewModel>(leaveType);
           return View(model);*/
        }

        // POST: LeaveTypesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, LeaveTypeViewModel model)
        {
            try
            {
                var type = await Repository.findByIDAsync(id);
                if (type == null)
                {
                    return NotFound();
                }
                bool isSuccess = await Repository.deleteAsync(type);
                if (!isSuccess)
                {
                    return View(model);
                }


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }
    }
}
