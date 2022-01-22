using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Management.DataAccess;
using Project_Management.Models;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Project_Management.Controllers
{
    [Authorize]
    public class DepertmentController : Controller
    {
        private readonly DepertmentsRepo _dptRpo;
        private readonly InstituteRepo _insRepo;

        public DepertmentController(DepertmentsRepo depertmentsRepo, InstituteRepo instituteRepo)
        {
            _dptRpo = depertmentsRepo;
            _insRepo = instituteRepo;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var v = await _dptRpo.GetAllRecords();
            return View(v.AsQueryable().Where(x => x.Status == true));
        }

        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Depertment depertment)
        {
            depertment.CreatedBy = User.FindFirstValue(ClaimTypes.Name);
            depertment.Status = true;
            depertment.CreatedDate = DateTime.Now;

            await _dptRpo.CreateNewRecord(depertment);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            return View(await _dptRpo.GetRecordById(id));
        }
        [HttpPost]
        public async Task<ActionResult> Edit(Depertment depertment)
        {
            depertment.CreatedBy = User.FindFirstValue(ClaimTypes.Name);
            depertment.Status = true;
            depertment.CreatedDate = DateTime.Now;


            await _dptRpo.UpdateRecord(depertment);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _dptRpo.GetRecordById(id));
        }

        [HttpPost]
        public async Task<ActionResult> Delete(Depertment depertment)
        {
            depertment.LastModifiedBy = User.FindFirstValue(ClaimTypes.Name);
            depertment.Status = false;
            depertment.LastModifiedDate = DateTime.Now;

            await _dptRpo.UpdateRecord(depertment);
            return RedirectToAction("Index");
        }
    }
}
