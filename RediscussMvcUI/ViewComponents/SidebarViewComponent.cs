using AhlatciProject.MvcUi.Areas.Admin.HttpApiServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using RediscussMvcUI.Extensions;
using RediscussMvcUI.Models;

namespace RediscussMvcUI.ViewComponents
{
	public class SidebarViewComponent : ViewComponent
	{
		private readonly IHttpApiService _httpApiService;

		public SidebarViewComponent(IHttpApiService httpApiService)
		{
			_httpApiService = httpApiService;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var user = HttpContext.Session.GetObject<UserItem>("ActiveUser");

			var joinsResponse = await _httpApiService.GetData<ResponseBody<List<JoinItem>>>($"/Joins/getByUserId?userId={user.UserId}", user.Token);
			return View(joinsResponse.Data);
		}
	}
}
