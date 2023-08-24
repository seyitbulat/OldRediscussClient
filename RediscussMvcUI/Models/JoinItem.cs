namespace RediscussMvcUI.Models
{
	public class JoinItem
	{
		public int UserId { get; set; }
		public int SubredisId { get; set; }
		public DateTime JoinedAt { get; set; }
		public string SubredisName { get; set; }

		public UserItem User { get; set; } = null;
		public SubredisItem Subredis { get; set; } = null;
    }
}
