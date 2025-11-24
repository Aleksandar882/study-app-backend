using System.Text.Json.Serialization;

namespace StudyApp.Models
{
    public class UserProgress
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        [JsonIgnore]
        public User? User { get; set; }

        public int LessonId { get; set; }

        [JsonIgnore]
        public Lesson? Lesson { get; set; }

        public bool Completed { get; set; } = false;

        public DateTime? CompletedAt { get; set; }
    }
}
