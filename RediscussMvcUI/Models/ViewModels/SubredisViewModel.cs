namespace RediscussMvcUI.Models.ViewModels
{
	public class SubredisViewModel
	{
		public int Count { get; set; }
        public SubredisItem SubredisItem { get; set; }
        public List<PostItem> PostItems { get; set; }
        public List<PostImageItem> PostImageItems { get; set; }
    }
}
