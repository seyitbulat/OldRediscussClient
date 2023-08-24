namespace RediscussMvcUI.Models.ViewModels
{
    public class HomeViewModel
    {
        public UserItem User { get; set; }
        public List<PostItem> PostItems { get; set; }
        public List<JoinItem> JoinItems { get; set; }
        public List<PostImageItem> PostImageItems { get; set; }
        public List<CommentItem> CommentItems { get; set; }
        public List<SubredisItem> Suggestions { get; set; }
    }
}
