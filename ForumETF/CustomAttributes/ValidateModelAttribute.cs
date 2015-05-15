using System.Web.Mvc;

namespace ForumETF.CustomAttributes
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Controller.ViewData.ModelState.IsValid)
            {
                return;
            }

            filterContext.Result = new ViewResult()
            {
                ViewName = filterContext.ActionDescriptor.ActionName,
                ViewData = filterContext.Controller.ViewData,
                TempData = filterContext.Controller.TempData
            };
        }
    }
}