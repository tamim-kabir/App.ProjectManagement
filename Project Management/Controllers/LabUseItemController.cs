﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Management.Controllers
{
    public class LabUseItemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}