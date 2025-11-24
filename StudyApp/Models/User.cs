namespace StudyApp.Models
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public List<UserProgress> ProgressRecords { get; set; } = new();

        public string Role { get; set; } = "User";
    }
}
