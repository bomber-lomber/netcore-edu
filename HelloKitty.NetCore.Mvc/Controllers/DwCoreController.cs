using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HelloKitty.NetCore.Mvc.Controllers
{
    public class DwCoreController : Controller
    {
        public IActionResult Index()
        {
            var model = DateTime.UtcNow.ToString(Dynamicweb.Core.Helpers.DateHelper.DateFormatString);
            return View((object)model);
        }

        public IActionResult MyBlazor()
        {
            return View();
        }
    }
}