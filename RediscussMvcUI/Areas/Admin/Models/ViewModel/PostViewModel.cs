using RediscussMvcUI.Models;

namespace RediscussMvcUI.Areas.Admin.Models.ViewModel
{
	public class PostViewModel
	{
		public List<PostItem> PostItems { get; set; }	
		public List<PostImageItem> PostImages { get; set; }
	}
}
