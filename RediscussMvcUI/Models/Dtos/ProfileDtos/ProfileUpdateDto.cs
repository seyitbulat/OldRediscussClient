namespace RediscussMvcUI.Models.Dtos.ProfileDtos
{
    public class ProfileUpdateDto
    {
        public int UserId { get; set; }
        public string ContentType { get; set; }
        public string? About { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
       // public DateOnly? DateOfBirth { get; set; }
        public string? BirthDate { get; set; }
        public string? Country { get; set; }
    }
}
