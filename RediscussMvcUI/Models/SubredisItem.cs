namespace RediscussMvcUI.Models
{
	public class SubredisItem
	{
		public int SubredisId { get; set; }
		public string SubredisName { get; set; }
		public string SubredisDescription { get; set; }
		public DateTime CreatedAt { get; set; }
		public int CreatedBy { get; set; }
		public string SubredisImage { get; set; }

        public List<PostItem> PostItems { get; set; }
    }
}
