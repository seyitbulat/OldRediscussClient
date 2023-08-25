using Microsoft.AspNetCore.Mvc;
using static RediscussMvcUI.Areas.Admin.Filters.AdminSessionAspect;

namespace RediscussMvcUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[AdminSesionAspect]
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
