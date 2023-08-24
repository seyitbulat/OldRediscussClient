namespace RediscussMvcUI.Models.Dtos.PostDtos
{
    public class NewPostDto
    {
        public string PostTitle { get; set; }
        public string PostBody { get; set; }
        public int CreatedBy { get; set; }
        public int SubredisId { get; set; }
    }
}
