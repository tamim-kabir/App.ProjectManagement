using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_Management.Areas.Identity.Data;
using Project_Management.DataAccess;
using Project_Management.Models;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Project_Management.Controllers
{
    [Authorize]
    public class InstituteController : Controller
    {
        private readonly InstituteRepo _insRepo;
        public readonly UserManager<ProjectManagementUser> _userManager;

        public InstituteController(InstituteRepo institute, UserManager<ProjectManagementUser> userManager)
        {
            _insRepo = institute;
            _userManager = userManager;
        }
        public async Task<ActionResult> Index()
        {
            var v = await _insRepo.GetAllRecords();

            return View(v.AsQueryable().Where(x => x.Status == true));
        }

        [HttpGet]
        public ActionResult Create()
        {
            var list = from p in _userManager.Users
                       select new
                       {
                           UserId = p.Id,
                           UserName = p.UserName
                       };
            ViewBag.Users = new SelectList(list, "UserId", "UserName");

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Institute institute)
        {
            institute.CreatedBy = User.FindFirstValue(ClaimTypes.Name);
            //institute.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            institute.Status = true;
            institute.CreatedDate = DateTime.Now;
            var list = from p in _userManager.Users
                       select new
                       {
                           UserId = p.Id,
                           UserName = p.UserName
                       };
            ViewBag.Users = new SelectList(list, "UserId", "UserName");

            await _insRepo.CreateNewRecord(institute);
            return RedirectToAction("index");
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            return View(await _insRepo.GetRecordById(id));
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            return View(await _insRepo.GetRecordById(id));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Institute institute)
        {
            institute.LastModifiedBy = User.FindFirstValue(ClaimTypes.Name);
            institute.Status = true;
            institute.LastModifiedDate = DateTime.Now;

            await _insRepo.UpdateRecord(institute);

            return RedirectToAction("index");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _insRepo.GetRecordById(id));
        }

        [HttpPost]
        public async Task<ActionResult> Delete(Institute institute)
        {
            institute.LastModifiedBy = User.FindFirstValue(ClaimTypes.Name);
            institute.Status = true;
            institute.LastModifiedDate = DateTime.Now;

            await _insRepo.UpdateRecord(institute);

            return RedirectToAction("index");
        }

    }
}
