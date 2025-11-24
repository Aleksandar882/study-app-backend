namespace StudyApp.DTO
{
    public class AuthResultDto
    {
        public string Token { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;

        public string Role { get; set; } = "User";
    }
}
