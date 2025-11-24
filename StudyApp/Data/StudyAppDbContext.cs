using Microsoft.EntityFrameworkCore;
using StudyApp.Models;

namespace StudyApp.Data
{
    public class StudyAppDbContext : DbContext
    {
        public StudyAppDbContext(DbContextOptions<StudyAppDbContext> options) : base(options) { }

        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Lesson> Lessons => Set<Lesson>();
        public DbSet<UserProgress> UserProgresses => Set<UserProgress>();
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Title = "Getting started with C#", Description = "Intro course" },
                new Course { Id = 2, Title = "React Basics", Description = "Frontend fundamentals" }
            );

            modelBuilder.Entity<Lesson>().HasData(
                new Lesson { Id = 1, Title = "What is C#?", CourseId = 1, Content = "Short intro to C#." },
                new Lesson { Id = 2, Title = "Hello World in C#", CourseId = 1, Content = "Write your first program." },
                new Lesson { Id = 3, Title = "JSX and Components", CourseId = 2, Content = "Intro to React components." }
            );


        }
    }
}
