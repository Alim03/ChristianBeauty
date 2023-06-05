using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChristianBeauty.Areas.Admin.Controllers
{
    public class CategoryController : AdminBaseController
    {
        public CategoryController() { }

        public IActionResult Index()
        {
            return View();
        }
    }
}
