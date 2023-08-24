namespace RediscussMvcUI.Models
{
	public class PostItem
	{
		public int PostId { get; set; }
		public string PostTitle { get; set; }
		public string PostBody { get; set; }
		public string PostImage { get; set; }
		public DateTime CreatedAt { get; set; }
		public int CreatedBy { get; set; }
		public int SubredisId { get; set; }


        public string SubredisName { get; set; }
        public string PostedByImage { get; set; }
        public string PostedBy { get; set; }

        public List<CommentItem> Comments { get; set; }
	}
}
