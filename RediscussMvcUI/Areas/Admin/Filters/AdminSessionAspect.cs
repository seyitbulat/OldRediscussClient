using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace RediscussMvcUI.Areas.Admin.Filters
{
    public class AdminSessionAspect
    {
        public class AdminSesionAspect : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext context)
            {
                var sessionJson = context.HttpContext.Session.GetString("AdminUser");
                if (string.IsNullOrEmpty(sessionJson))
                {
                    context.Result = new RedirectResult("/Admin/Authentication/Login");
                }
            }
        }
    }
}
