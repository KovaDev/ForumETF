using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace ForumETF.Controllers
{   
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            // Ovo je radilo u prvom slucaju bez citanja iz baze
            //var userClaims = User.Identity as ClaimsIdentity;
            //ViewBag.Country = userClaims.FindFirst(ClaimTypes.Country).Value;

            return View();
        }


    }
}