﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.Controllers
{

    public class AdministratorController : Controller
    {
        [Authorize(Roles = "Administrator")]
        public IActionResult HR()
        {
            return View("Index");
        }






    }
}
