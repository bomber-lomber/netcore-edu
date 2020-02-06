using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelloKitty.NetFramework.Mvc.Controllers
{
    public class DwCoreController : Controller
    {
        // GET: DwCore
        public ActionResult Index()
        {
            var model = DateTime.UtcNow.ToString(Dynamicweb.Core.Helpers.DateHelper.DateFormatString);
            return View((object)model);
        }
    }
}