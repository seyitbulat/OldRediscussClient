
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using RediscussMvcUI.Areas.Admin.Models;
using RediscussMvcUI.Extensions;
using System.Text.Json;

namespace AhlatciProject.MvcUi.Areas.Admin.ViewComponents
{
    [Area("Admin")]
    public class SideBarViewComponent : ViewComponent
    {
        public ViewViewComponentResult Invoke()
        {
            var adminUser = HttpContext.Session.GetObject<AdminUserItem>("AdminUser");
           
            return View(adminUser);
        }
    }
}
