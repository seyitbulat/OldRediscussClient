using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace RediscussMvcUI.ViewComponents
{
	public class RightbarViewComponent : ViewComponent
	{
		public ViewViewComponentResult Invoke()
		{
			return View();
		}
	}
}
