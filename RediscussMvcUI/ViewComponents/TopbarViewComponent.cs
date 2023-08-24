using AhlatciProject.MvcUi.Areas.Admin.HttpApiServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using RediscussMvcUI.Extensions;
using RediscussMvcUI.Models;

namespace RediscussMvcUI.ViewComponents
{
	public class TopbarViewComponent : ViewComponent
	{
        private readonly IHttpApiService _httpApiService;

        public TopbarViewComponent(IHttpApiService httpApiService)
        {
            _httpApiService = httpApiService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
		{
			var sessionUser = HttpContext.Session.GetObject<UserItem>("ActiveUser");

			var userResponse = await _httpApiService.GetData<ResponseBody<UserItem>>($"/Users/{sessionUser.UserId}", sessionUser.Token);

			return View(userResponse.Data);
		}
	}
}
