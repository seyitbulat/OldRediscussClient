namespace RediscussMvcUI.Models.Dtos
{
    public class CommentSendDto
    {
        public int PostId { get; set; }
        public int CreatedBy { get; set; }
        public string CommentBody { get; set; }
    }
}
