using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyLeaveManagement.Contracts;
using MyLeaveManagement.Data;
using MyLeaveManagement.Models;

namespace MyLeaveManagement.Controllers
{
    [Authorize(Roles ="Admin")]
    public class LeaveTypesController : Controller
    {
        private readonly IMapper mapper;
        private readonly ILeaveTypeRepository Repository;
        public  LeaveTypesController(IMapper mapper,ILeaveTypeRepository repository)
        {
            this.mapper = mapper;
            Repository=repository;
        }
        // GET: LeaveTypesController
        public ActionResult Index()
        {
            var leavetypes=Repository.GetAll().ToList();
            var model = mapper.Map < List<LeaveType>, List < LeaveTypeViewModel >>(leavetypes);
            return View(model);
        }

        // GET: LeaveTypesController/Details/5
        public ActionResult Details(int id)
        {
            if(!Repository.isExists(id))
            {
                return NotFound();
            }
            var leavetype= Repository.findByID(id);
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
        public ActionResult Create(LeaveTypeViewModel model)
        {
            try
            {
                if(!ModelState.IsValid) 
                {
                    return View(model);
                }
                var leaveType=mapper.Map<LeaveType>(model);
                leaveType.DateCreated= DateTime.Now;
                var isSuccess=Repository.Create(leaveType);
                if (!isSuccess) 
                {
                    ModelState.AddModelError("","something went wrong");
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
        public ActionResult Edit(int id)
        {
            if (!Repository.isExists(id))
            {
                return NotFound();
            }
            var leaveType=Repository.findByID(id);
            var model = mapper.Map<LeaveTypeViewModel>(leaveType);
            return View(model);
        }

        // POST: LeaveTypesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LeaveTypeViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var leaveType = mapper.Map<LeaveType>(model);
                var isSuccess = Repository.update(leaveType);
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
        public ActionResult Delete(int id)
        {
            var type = Repository.findByID(id);
            if (type == null)
            {
                return NotFound();
            }
            bool isSuccess = Repository.delete(type);
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
        public ActionResult Delete(int id, LeaveTypeViewModel model)
        {
            try
            {
                var type = Repository.findByID(id);
                if (type == null)
                {
                    return NotFound();
                }
                bool isSuccess =Repository.delete(type);
                if(!isSuccess)
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
