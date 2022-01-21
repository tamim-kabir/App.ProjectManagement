using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_Management.DataAccess;
using Project_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Project_Management.Controllers
{
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
            return View(v.Where(x => x.Status == true));
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var list = from p in await _insRepo.GetAllRecords()
                       select new
                       {
                           InstituteId = p.InstituteId,
                           InstituteName = p.InstituteName
                       };

            ViewBag.Institute = new SelectList(list, "InstituteId", "InstituteName");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Depertment depertment)
        {
            depertment.CreatedBy = User.FindFirstValue(ClaimTypes.Name);
            depertment.Status = true;
            depertment.CreatedDate = DateTime.Now;

            var list = from p in await _insRepo.GetAllRecords()
                       select new
                       {
                           InstituteId = p.InstituteId,
                           InstituteName = p.InstituteName
                       };

            ViewBag.Institute = new SelectList(list, "InstituteId", "InstituteName");

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
