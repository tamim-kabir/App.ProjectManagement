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
    public class ItemsController : Controller
    {
        private readonly ItemsRepo _itemsRepo;
        public ItemsController(ItemsRepo itemsRepo)
        {
            this._itemsRepo = itemsRepo;
        }
        
        public async Task<ActionResult> Index()
        {
            var v = await _itemsRepo.GetAllRecords();

            return View(v.Where(x => x.Status == true));
        }

        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Items item)
        {
            item.CreatedBy = User.FindFirstValue(ClaimTypes.Name);
            item.Status = true;
            item.CreatedDate = DateTime.Now;
            await _itemsRepo.CreateNewRecord(item);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(int id)
        {
            return View(await _itemsRepo.GetRecordById(id));
        }

        public async Task<ActionResult> Edit(int id)
        {

            return View(await _itemsRepo.GetRecordById(id)); 
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Items item)
        {
            item.LastModifiedDate = DateTime.Now;
            item.LastModifiedBy = User.FindFirstValue(ClaimTypes.Name);
            await _itemsRepo.UpdateRecord(item);

            return RedirectToAction("Index");
        }

 
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _itemsRepo.GetRecordById(id));
        }

        [HttpPost]
        public async Task<ActionResult> Delete(Items items)
        {
            items.Status = false;
            items.LastModifiedDate = DateTime.Now;

            await _itemsRepo.UpdateRecord(items);
            return RedirectToAction("Index");
        }
    }
}
