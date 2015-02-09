using System.Web.Mvc;
using System.Web.Routing;

namespace ForumETF
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                "PostsByTag",
                "Post/Tags/{tagName}",
                new { controller = "Post", action = "GetPostsByTag" });

            routes.MapRoute(
                "PostsByCategory",
                "Post/Category/{categoryName}",
                new { controller = "Post", action = "GetPostsByCategory" });

            routes.MapRoute(
                "HomePage",
                "Home/Index/page/{page}",
                new { controller = "Home", action = "Index" });

            routes.MapRoute(
                "PostDetails",
                "Post/Details/{postId}/{seoName}",
                new { controller = "Post", action = "Details", seoName = "" },
                new { postId = @"^\d+$" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
