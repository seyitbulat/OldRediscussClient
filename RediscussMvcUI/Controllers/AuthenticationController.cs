using AhlatciProject.MvcUi.Areas.Admin.HttpApiServices;
using Microsoft.AspNetCore.Mvc;
using RediscussMvcUI.Extensions;
using RediscussMvcUI.Models;
using RediscussMvcUI.Models.Dtos.AuthenticationDtos;
using System.Text.Json;

namespace RediscussMvcUI.Controllers
{
	public class AuthenticationController : Controller
	{
		private readonly IHttpApiService _httpApiService;

		public AuthenticationController(IHttpApiService httpApiService)
		{
			_httpApiService = httpApiService;
		}

		[HttpGet]
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

			if(response.StatusCode == 200)
			{
				HttpContext.Session.SetObject("ActiveUser", response.Data);
				return Json(new {IsSuccess = true, Message = "Giris Basarili"});
			}
			return Json(new { IsSuccess = false, Message = response.ErrorMessages });

		}

		public async Task<IActionResult> Logout()
		{
			HttpContext.Session.Remove("ActiveUser");
			return RedirectToAction("Login");
		}

		public IActionResult Signup()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Signup(SignupDto dto)
		{
			var postObject = new
			{
				UserName = dto.UserName,
				Password = dto.Password,
				Email = dto.Email
			};

			var response = await _httpApiService.PostData<ResponseBody<UserItem>>($"/Users/signup", JsonSerializer.Serialize(postObject));
			if (response.StatusCode == 201)
			{
				HttpContext.Session.SetObject("ActiveUser", response.Data);
				return Json(new { IsSuccess = true, Message = "Giris Basarili" });
			}
			return Json(new { IsSuccess = false, Message = response.ErrorMessages });
		}
	}
}
