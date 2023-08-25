namespace RediscussMvcUI.Models
{
    public class CommentItem
    {

        public int CommentId { get; set; }
        public string CommentBody { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public int PostId { get; set; }

        public string CreatedByName { get; set; }
        public string ImageRoute { get; set; }
        public string UserImage { get; set; }
        public string ContentType
        {
            get
            {
                if(ImageRoute != null)
                {
                    return $"image/{Path.GetExtension(ImageRoute)}";
                }
                return string.Empty;
            }
        }
    }
}
