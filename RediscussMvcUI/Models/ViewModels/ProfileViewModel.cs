namespace RediscussMvcUI.Models.ViewModels
{
    public class ProfileViewModel
    {
        public UserItem UserItem { get; set; }
        public List<JoinItem> JoinItems { get; set; }
        public List<PostItem> PostItems { get; set; }
        public List<PostImageItem> PostImageItems { get; set; }
    }
}
