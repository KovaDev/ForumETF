using System.Web.Mvc;
using ForumETF.CustomAttributes;

namespace ForumETF.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Admin/Dashboard
        //[Authorize(Roles = "Admin")]
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }
    }
}