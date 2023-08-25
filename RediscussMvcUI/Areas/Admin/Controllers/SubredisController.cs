using AhlatciProject.MvcUi.Areas.Admin.HttpApiServices;
using Microsoft.AspNetCore.Mvc;
using RediscussMvcUI.Extensions;
using RediscussMvcUI.Models;
using static RediscussMvcUI.Areas.Admin.Filters.AdminSessionAspect;

namespace RediscussMvcUI.Areas.Admin.Controllers
{
	[Area("Admin")]
    [AdminSesionAspect]

    public class SubredisController : Controller
	{
		private readonly IWebHostEnvironment _webHost;
		private readonly IHttpApiService _httpApiService;

		public SubredisController(IWebHostEnvironment webHost, IHttpApiService httpApiService)
		{
			_webHost = webHost;
			_httpApiService = httpApiService;
		}

		public async Task<IActionResult> Index()
		{
			var user = HttpContext.Session.GetObject<UserItem>("AdminUser");
			var response = await _httpApiService.GetData<ResponseBody<List<SubredisItem>>>("/Subredises", user.Token);

			return View(response.Data);
		}

		public async Task<IActionResult> Delete(int id)
		{
			var user = HttpContext.Session.GetObject<UserItem>("AdminUser");
			var response = await _httpApiService.DeleteData<ResponseBody<NoData>>($"Subredises?id={id}", user.Token);

			if (response.StatusCode.ToString().StartsWith("20"))
			{
				return Json(new { IsSuccessful = true, Message = "Operation successful" });
			}
			return Json(new { IsSuccessful = true, Message = response.ErrorMessages });

		}
	}
}
