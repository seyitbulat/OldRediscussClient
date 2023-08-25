using AhlatciProject.MvcUi.Areas.Admin.HttpApiServices;
using Microsoft.AspNetCore.Mvc;
using RediscussMvcUI.Extensions;
using RediscussMvcUI.Models;
using RediscussMvcUI.Models.Dtos.PostDtos;
using System.Text.Json;

namespace RediscussMvcUI.Controllers
{
    public class PostController : Controller
    {
        private readonly IHttpApiService _httpApiService;
        private readonly IWebHostEnvironment _webHost;

        public PostController(IHttpApiService httpApiService, IWebHostEnvironment webHost)
        {
            _httpApiService = httpApiService;
            _webHost = webHost;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewPost(NewPostDto dto, IFormFileCollection files)
        {
            var user = HttpContext.Session.GetObject<UserItem>("ActiveUser");
            dto.CreatedBy = user.UserId;

            var postResponse = await _httpApiService.PostData<ResponseBody<PostItem>>("/Posts",JsonSerializer.Serialize(dto),user.Token);

            var content = new MultipartFormDataContent();

            foreach(var file in files)
            {
                var fileName = Path.GetFileName(file.FileName);
                var tempRoute = $@"{_webHost.ContentRootPath}/wwwroot/tempFiles/{fileName}";
                using var fs = new FileStream(tempRoute, FileMode.Create);
                file.CopyTo(fs);

                fs.Close();
                var type = file.ContentType.ToString();
                var fileStream = System.IO.File.Open(tempRoute, FileMode.Open);
                content.Add(new StreamContent(fileStream), "File", fileName);
                content.Add(new StringContent($"{type}"), "ContentType");
            }

            var imageResponse = await _httpApiService.PostData<ResponseBody<PostImageItem>>($"PostImages?PostId={postResponse.Data.PostId}", content, user.Token);

            if (postResponse.StatusCode.ToString().StartsWith("20"))
            {
                return Json(new { IsSuccess = true, Message = "Post successful" });
            }
            return Json(new {IsSuccess = false, Message = imageResponse.ErrorMessages});
        }
    }
}
