using AhlatciProject.MvcUi.Areas.Admin.HttpApiServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using RediscussMvcUI.Extensions;
using RediscussMvcUI.Models;
using RediscussMvcUI.Models.ViewModels;

namespace RediscussMvcUI.Controllers
{
	public class SubredisController : Controller
	{
		private readonly IHttpApiService _httpApiService;

		public SubredisController(IHttpApiService httpApiService)
		{
			_httpApiService = httpApiService;
		}

		[HttpGet]
		public async Task<IActionResult> Index(int id)
		{
            var user = HttpContext.Session.GetObject<UserItem>("ActiveUser");

			var subredisResponse = await _httpApiService.GetData<ResponseBody<SubredisItem>>($"/Subredises/{id}", token: user.Token) ;
			var joinResponse = await _httpApiService.GetData<ResponseBody<List<JoinItem>>>($"/Joins/getBySubredisId?subredisId={id}", token: user.Token);
			var postResponse = await _httpApiService.GetData<ResponseBody<List<PostItem>>>($"/Posts/getBySubredis?subredisId={id}", token: user.Token);
            var joinsResponse = await _httpApiService.GetData<ResponseBody<List<JoinItem>>>($"/Joins/getByUserId?userId={user.UserId}", user.Token);
			var userResponse = await _httpApiService.GetData<ResponseBody<UserItem>>($"/Users/{user.UserId}", user.Token);
            ResponseBody<List<PostImageItem>> images = new ResponseBody<List<PostImageItem>>()
            {
                Data = new List<PostImageItem>(),
                ErrorMessages = new List<string>(),
                StatusCode = 0
            };


            foreach (var post in postResponse.Data)
            {
                var image = await _httpApiService.GetData<ResponseBody<List<PostImageItem>>>($"/PostImages/getByPostId/{post.PostId}", user.Token);

                if (image.Data.Count != 0 || image.Data != null)
                    images.Data.AddRange(image.Data);

            }

            SubredisViewModel md = new SubredisViewModel()
			{
				Count = joinResponse.Data.Count(),
				SubredisItem = subredisResponse.Data,
				PostItems = postResponse.Data,
				PostImageItems = images.Data,
				JoinItems = joinResponse.Data,
				UserItem = userResponse.Data
			};

			return View(md);
		}
	}
}
