using AhlatciProject.MvcUi.Areas.Admin.HttpApiServices;
using Microsoft.AspNetCore.Mvc;
using RediscussMvcUI.Areas.Admin.Models.AuthenticationDtos;
using RediscussMvcUI.Extensions;
using RediscussMvcUI.Models;
using System.Text.Json;

namespace RediscussMvcUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class AuthenticationController: Controller
	{
		private readonly IHttpApiService _httpApiService;

		public AuthenticationController(IHttpApiService httpApiService)
		{
			_httpApiService = httpApiService;
		}

		public IActionResult Login()
		{
			return View();
		}


		[HttpPost]
		public async Task<IActionResult> Login(LoginDto dto)
		{
			var postObject = new
			{
				UserName = dto.UserName,
				Password = dto.Password
			};
			var response = await _httpApiService.PostData<ResponseBody<UserItem>>($"/Users/adminLogin", JsonSerializer.Serialize(postObject));

			if (response.StatusCode == 200)
			{
				HttpContext.Session.SetObject("AdminUser", response.Data);
				return Json(new { IsSuccess = true, Message = "Login successful" });
			}
			return Json(new { IsSuccess = false, Message = response.ErrorMessages });
		}
	}
}
