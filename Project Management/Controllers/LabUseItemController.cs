using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Management.DataAccess;

namespace Project_Management.Controllers
{
    [Authorize]
    public class LabUseItemController : Controller
    {
        private readonly LabUseItemRepo _labRepo;

        public LabUseItemController(LabUseItemRepo labUseItemRepo)
        {
            _labRepo = labUseItemRepo;
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}
