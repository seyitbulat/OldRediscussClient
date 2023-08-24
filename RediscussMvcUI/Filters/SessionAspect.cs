using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RediscussMvcUI.Filters
{
	public class SessionAspect : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			var sessionJson = context.HttpContext.Session.GetString("ActiveUser");
			if(string.IsNullOrEmpty(sessionJson))
			{
				context.Result = new RedirectToActionResult("Login", "Authentication", null);
			}
		}
	}
}
