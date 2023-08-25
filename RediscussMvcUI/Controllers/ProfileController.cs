using AhlatciProject.MvcUi.Areas.Admin.HttpApiServices;
using Microsoft.AspNetCore.Mvc;
using RediscussMvcUI.Extensions;
using RediscussMvcUI.Models;
using RediscussMvcUI.Models.Dtos.ProfileDtos;
using RediscussMvcUI.Models.ViewModels;
using System.Text;
using System.Text.Json;

namespace RediscussMvcUI.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IHttpApiService _httpApiService;
        private readonly IWebHostEnvironment _webHost;

        public ProfileController(IHttpApiService httpApiService, IWebHostEnvironment webHost)
        {
            _httpApiService = httpApiService;
            _webHost = webHost;
        }

        public async Task<IActionResult> Index()
        {
            var sessionUser = HttpContext.Session.GetObject<UserItem>("ActiveUser");

            var userResponse = await _httpApiService.GetData<ResponseBody<UserItem>>($"/Users/{sessionUser.UserId}", sessionUser.Token);
            var joinResponse = await _httpApiService.GetData<ResponseBody<List<JoinItem>>>($"/Joins/getByUserId?userId={sessionUser.UserId}", sessionUser.Token);

            var postResponse = await _httpApiService.GetData<ResponseBody<List<PostItem>>>($"/Posts/getByUser?userId={sessionUser.UserId}", sessionUser.Token);

            ResponseBody<List<PostImageItem>> images = new ResponseBody<List<PostImageItem>>()
            {
                Data = new List<PostImageItem>(),
                ErrorMessages = new List<string>(),
                StatusCode = 0
            };


            foreach (var post in postResponse.Data)
            {
                var image = await _httpApiService.GetData<ResponseBody<List<PostImageItem>>>($"/PostImages/getByPostId/{post.PostId}", sessionUser.Token);

                if (image.Data.Count != 0 || image.Data != null)
                    images.Data.AddRange(image.Data);

            }

            ProfileViewModel vm = new()
            {
                UserItem = userResponse.Data,
                JoinItems = joinResponse.Data,
                PostItems = postResponse.Data,
                PostImageItems = images.Data
            };
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Profile(int id)
        {
            var sessionUser = HttpContext.Session.GetObject<UserItem>("ActiveUser");

            var userResponse = await _httpApiService.GetData<ResponseBody<UserItem>>($"/Users/{id}", sessionUser.Token);
            var joinResponse = await _httpApiService.GetData<ResponseBody<List<JoinItem>>>($"/Joins/getByUserId?userId={id}", sessionUser.Token);

            var postResponse = await _httpApiService.GetData<ResponseBody<List<PostItem>>>($"/Posts/getByUser?userId={id}", sessionUser.Token);

            ResponseBody<List<PostImageItem>> images = new ResponseBody<List<PostImageItem>>()
            {
                Data = new List<PostImageItem>(),
                ErrorMessages = new List<string>(),
                StatusCode = 0
            };


            foreach (var post in postResponse.Data)
            {
                var image = await _httpApiService.GetData<ResponseBody<List<PostImageItem>>>($"/PostImages/getByPostId/{post.PostId}", sessionUser.Token);

                if (image.Data.Count != 0 || image.Data != null)
                    images.Data.AddRange(image.Data);

            }

            ProfileViewModel vm = new()
            {
                UserItem = userResponse.Data,
                JoinItems = joinResponse.Data,
                PostItems = postResponse.Data,
                PostImageItems = images.Data
            };
            return View(vm);
        }

        public async Task<IActionResult> ProfileSetup()
        {
            var sessionUser = HttpContext.Session.GetObject<UserItem>("ActiveUser");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProfileSetup(IFormFile file, ProfileSetupDto dto)
        {
            var sessionUser = HttpContext.Session.GetObject<UserItem>("ActiveUser");

            var content = new MultipartFormDataContent();

            var fileName = file.FileName;
            var tempRoute = $@"{_webHost.ContentRootPath}/wwwroot/tempFiles/{fileName}";
            using var fs = new FileStream(tempRoute, FileMode.Create);
            file.CopyTo(fs);
            fs.Close();

            dto.UserId = sessionUser.UserId;

            var type = file.ContentType.ToString();
            var fileStream = System.IO.File.Open(tempRoute, FileMode.Open);
            content.Add(new StreamContent(fileStream), "File", fileName);
            content.Add(new StringContent(dto.FirstName), "FirstName");
            content.Add(new StringContent(dto.LastName), "LastName");
            content.Add(new StringContent(dto.Gender), "Gender");
            content.Add(new StringContent(dto.Country), "Country");
            content.Add(new StringContent(dto.UserId.ToString()), "UserId");

            var userResponse = await _httpApiService.PatchData<ResponseBody<NoData>>($"/Users/profileSetup", content, sessionUser.Token);

            return Json(new { IsSuccess = true, message = "Successfull" });
        }

        public async Task<IActionResult> ProfileEdit()
        {
            var sessionUser = HttpContext.Session.GetObject<UserItem>("ActiveUser");

            var userResponse = await _httpApiService.GetData<ResponseBody<UserItem>>($"/Users/{sessionUser.UserId}", sessionUser.Token);

            return View(userResponse.Data);
        }
        [HttpPost]
        public async Task<IActionResult> ProfileEdit(IFormFile file, ProfileUpdateDto dto)
        {
            var sessionUser = HttpContext.Session.GetObject<UserItem>("ActiveUser");

            var content = new MultipartFormDataContent();

            var fileName = file.FileName;
            var tempRoute = $@"{_webHost.ContentRootPath}/wwwroot/tempFiles/{fileName}";
            using var fs = new FileStream(tempRoute, FileMode.Create);
            file.CopyTo(fs);
            fs.Close();
            dto.UserId = sessionUser.UserId;
            var dtoJson = JsonSerializer.Serialize(dto);

            dto.ContentType = file.ContentType;

            var type = file.ContentType.ToString();
            var fileStream = System.IO.File.Open(tempRoute, FileMode.Open);
            content.Add(new StreamContent(fileStream), "File", fileName);
            content.Add(new StringContent(dto.UserId.ToString()), "UserId");
            content.Add(new StringContent(dto.ContentType), "ContentType");
            content.Add(new StringContent(dto.About), "About");
            content.Add(new StringContent(dto.FirstName), "FirstName");
            content.Add(new StringContent(dto.LastName), "LastName");
            content.Add(new StringContent(dto.Gender), "Gender");
            content.Add(new StringContent(dto.BirthDate), "BirthDate");
            content.Add(new StringContent(dto.Country), "Country");



            var userResponse = await _httpApiService.PatchData<ResponseBody<NoData>>($"/Users", content, sessionUser.Token);
            return null ;
        }
    }
}
