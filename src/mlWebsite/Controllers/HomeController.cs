using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace mlWebsite.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (this.User == null || !this.User.Identity.IsAuthenticated)
            {
                return View("Index");
            }
            else {
                return View("MlApp");
            }
        }
    }
}
