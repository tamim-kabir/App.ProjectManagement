using ExcelDataReader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_Management.DataAccess;
using Project_Management.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace Project_Management.Controllers
{
    [Authorize]
    public class ItemsController : Controller
    {
        private readonly IWebHostEnvironment _hostEnv;
        private readonly ItemsRepo _itemsRepo;
        private readonly DepertmentsRepo _dptRpo;

        public ItemsController(IWebHostEnvironment webHost, ItemsRepo itemsRepo, DepertmentsRepo depertmentsRepo)
        {
            this._itemsRepo = itemsRepo;
            this._dptRpo = depertmentsRepo;
            _hostEnv = webHost;
        }

        public ActionResult UploadsExcelSheet()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> UploadsExcelSheetCreate()
        {
            try
            {
                var iFileCollection = HttpContext.Request.Form.Files;
                var file = iFileCollection.Take(1).FirstOrDefault();
                string filename = iFileCollection.Take(1).Select(x => x.FileName).FirstOrDefault().ToString();

                var filePath = Path.Combine(_hostEnv.ContentRootPath, "File", filename);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        int i = 0;
                        while (reader.Read()) //Each row of the file
                        {
                            var value = reader.GetValue(0);
                            if (i > 0 && value != null)
                            {
                                Items item = new Items();
                                item.ItemName = reader.GetValue(0).ToString();
                                item.DppQty = int.Parse(reader.GetValue(1).ToString());
                                item.DppPrice = decimal.Parse(reader.GetValue(2).ToString());
                                item.Description = reader.GetValue(3).ToString();
                                item.DepertmentId = int.Parse(reader.GetValue(4).ToString());
                                item.CreatedDate = DateTime.Now;
                                item.CreatedBy = User.FindFirstValue(ClaimTypes.Name);
                                item.Status = true;

                                await _itemsRepo.CreateNewRecord(item);
                            }
                            i++;
                        }
                    }
                }
                if ((System.IO.File.Exists(filePath)))
                {
                    System.IO.File.Delete(filePath);
                }

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {

                throw e;
            }           
        }


        public async Task<ActionResult> Index()
        {
            var v = await _itemsRepo.GetAllRecords();

            return View(v.AsQueryable().Where(x => x.Status == true));
        }

        public async Task<ActionResult> Create()
        {
            var list = from p in await _dptRpo.GetAllRecords()
                       select new
                       {
                           UserId = p.Id,
                           UserName = p.Name
                       };
            ViewBag.DeptDropDown = new SelectList(list, "UserId", "UserName");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Items item)
        {
            item.CreatedBy = User.FindFirstValue(ClaimTypes.Name);
            item.Status = true;
            item.CreatedDate = DateTime.Now;
            var list = from p in await _dptRpo.GetAllRecords()
                       select new
                       {
                           UserId = p.Id,
                           UserName = p.Name
                       };
            ViewBag.DeptDropDown = new SelectList(list, "UserId", "UserName");

            await _itemsRepo.CreateNewRecord(item);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(int id)
        {
            return View(await _itemsRepo.GetRecordById(id));
        }

        public async Task<ActionResult> Edit(int id)
        {
            var list = from p in await _dptRpo.GetAllRecords()
                       select new
                       {
                           UserId = p.Id,
                           UserName = p.Name
                       };
            ViewBag.DeptDropDown = new SelectList(list, "UserId", "UserName");

            return View(await _itemsRepo.GetRecordById(id));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Items item)
        {
            item.LastModifiedDate = DateTime.Now;
            item.LastModifiedBy = User.FindFirstValue(ClaimTypes.Name);
            await _itemsRepo.UpdateRecord(item);

            var list = from p in await _dptRpo.GetAllRecords()
                       select new
                       {
                           UserId = p.Id,
                           UserName = p.Name
                       };
            ViewBag.DeptDropDown = new SelectList(list, "UserId", "UserName");


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
