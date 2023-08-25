namespace RediscussMvcUI.Models
{
    public class UserItem
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int Discuit { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UserImage { get; set; }
        public string? ImageRoute { get; set; }
        public string? About { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? BirthDate { get; set; }
        public string? Country { get; set; }
        public bool? IsActive { get; set; }

        public string? ImageContentType
        {
            get
            {
                if(ImageRoute != null && ImageRoute.Contains("."))
                {
                    return $"image/{Path.GetExtension(ImageRoute)}";
                }
                return string.Empty ;
            }
        }

        public string? FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
        public string Token { get; set; }
    }
}
