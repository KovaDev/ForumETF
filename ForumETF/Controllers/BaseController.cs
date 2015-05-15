using System.Threading.Tasks;
using System.Web.Mvc;
using ForumETF.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ForumETF.Controllers
{
    public class BaseController : Controller
    {
        protected readonly AppDbContext context;
        protected readonly UserManager<AppUser> manager;

        public BaseController()
        {
            context = new AppDbContext();
            manager = new UserManager<AppUser>(new UserStore<AppUser>(context));
        }

        protected async Task<AppUser> GetCurrentUser()
        {
            return await manager.FindByIdAsync(User.Identity.GetUserId());
        }

    }
}