using AhlatciProject.MvcUi.Areas.Admin.HttpApiServices;
using Microsoft.AspNetCore.Mvc;
using RediscussMvcUI.Extensions;
using RediscussMvcUI.Filters;
using RediscussMvcUI.Models;
using RediscussMvcUI.Models.Dtos;
using RediscussMvcUI.Models.Dtos.JoinDtos;
using RediscussMvcUI.Models.ViewModels;
using System.Diagnostics;
using System.Text.Json;

namespace RediscussMvcUI.Controllers
{
    [SessionAspect]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpApiService _httpApiService;

        public HomeController(ILogger<HomeController> logger, IHttpApiService httpApiService)
        {
            _logger = logger;
            _httpApiService = httpApiService;
        }

        public async Task<IActionResult> Index()
        {
            var user = HttpContext.Session.GetObject<UserItem>("ActiveUser");
            var posts = await _httpApiService.GetData<ResponseBody<List<PostItem>>>($"/Posts/getByJoinedUser?userId={user.UserId}", token: user.Token);

            var joinedSubredises = await _httpApiService.GetData<ResponseBody<List<JoinItem>>>($"/Joins/getByUserId?userId={user.UserId}", user.Token);

            ResponseBody<List<PostImageItem>> images = new ResponseBody<List<PostImageItem>>() { 
                Data = new List<PostImageItem>(),
                ErrorMessages = new List<string>(),
                StatusCode = 0
            };
            
            ResponseBody<List<CommentItem>> comments = new ResponseBody<List<CommentItem>>() { Data = new List<CommentItem>(), ErrorMessages = new List<string>(), StatusCode = 0 };

            foreach (var post in posts.Data)
            {
                var image = await _httpApiService.GetData<ResponseBody<List<PostImageItem>>>($"/PostImages/getByPostId/{post.PostId}", user.Token);
                var comment = await _httpApiService.GetData<ResponseBody<List<CommentItem>>>($"/Comments/getByPostId?postId={post.PostId}",user.Token);

                if(image.Data.Count != 0 || image.Data != null)
                    images.Data.AddRange(image.Data);

                if(comment.Data.Count != 0 || comment.Data != null)
                {
                    comments.Data.AddRange(comment.Data);
                }
            }

            var suggestions = await _httpApiService.GetData<ResponseBody<List<SubredisItem>>>($"/Subredises/getSuggestion?userId={user.UserId}", user.Token);

            var activeUser = await _httpApiService.GetData<ResponseBody<UserItem>>($"/Users/{user.UserId}",user.Token);
            HomeViewModel md = new HomeViewModel()
            {
                PostItems = posts.Data,
                JoinItems = joinedSubredises.Data,
                PostImageItems = images.Data,
                CommentItems = comments.Data,
                Suggestions = suggestions.Data,
                User = activeUser.Data
            };

            return View(md);
        }
        

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> JoinSubredis(int id)
        {
            var userSession = HttpContext.Session.GetObject<UserItem>("ActiveUser");

            JoinSubredisDto dto = new JoinSubredisDto() { SubredisId = id , UserId = userSession.UserId};

            var joinResponse = await _httpApiService.PostData<JoinItem>("/Joins", JsonSerializer.Serialize(dto), userSession.Token);

            return Json(new {IsSuccess = true});
        }

        public async Task<IActionResult> SendComment(CommentSendDto dto)
        {
            var userSession = HttpContext.Session.GetObject<UserItem>("ActiveUser");
            dto.CreatedBy = userSession.UserId;

            var response = await _httpApiService.PostData<ResponseBody<CommentItem>>("/Comments", JsonSerializer.Serialize(dto), userSession.Token);

            if(response.ErrorMessages == null)
            {
                return Json(new {IsSuccess = true, Message = "Succesfully commented"});
            }
            return Json(new { IsSuccess = false, Message = response.ErrorMessages });

        }
    }
}