using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelloKitty.NetCore.Mvc.Controllers
{
    public class MyBlazorController : Controller
    {
        // GET: MyBlazor
        public ActionResult AppHost()
        {
            return View("_Host");
        }

        // GET: MyBlazor
        public ActionResult Counter()
        {
            return View();
        }

        public ActionResult Dialog()
        {
            return View();
        }
    }
}