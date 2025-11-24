using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyApp.Data;
using StudyApp.Models;
using System.Security.Claims;

namespace StudyApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserProgressController : ControllerBase
    {
        private readonly StudyAppDbContext _db;

        public UserProgressController(StudyAppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _db.UserProgresses.Include(u => u.Lesson).ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        { 
            var userProgress = await _db.UserProgresses.Include(u => u.Lesson).FirstOrDefaultAsync(u => u.Id == id);
            return userProgress is null ? NotFound() : Ok(userProgress);

        }

        [HttpPost]
        public async Task<IActionResult> Create(UserProgress userProgress)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var lessonExists = await _db.Lessons.AnyAsync(l => l.Id == userProgress.LessonId);
            if (!lessonExists)
                return BadRequest("Lesson not found.");

            var existingProgress = await _db.UserProgresses
                .FirstOrDefaultAsync(u => u.UserId == userProgress.UserId && u.LessonId == userProgress.LessonId);

            if (existingProgress != null)
                return Conflict("Progress for this lesson already exists for this user.");

            userProgress.UserId = userId;
            if (userProgress.Completed && userProgress.CompletedAt == null)
                userProgress.CompletedAt = DateTime.UtcNow;

            _db.UserProgresses.Add(userProgress);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = userProgress.Id }, userProgress);
        }

        [HttpPost("complete/{lessonId:int}")]
        public async Task<IActionResult> CompleteLesson(int lessonId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var lesson = await _db.Lessons.FindAsync(lessonId);
            if (lesson == null)
                return NotFound("Lesson not found.");

            var existing = await _db.UserProgresses
                .FirstOrDefaultAsync(p => p.UserId == userId && p.LessonId == lessonId);

            if (existing != null)
                return BadRequest("Lesson already marked as completed.");

            var progress = new UserProgress
            {
                UserId = userId,
                LessonId = lessonId,
                Completed = true,
                CompletedAt = DateTime.UtcNow
            };

            _db.UserProgresses.Add(progress);
            await _db.SaveChangesAsync();

            return Ok(progress);
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyProgress()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var list = await _db.UserProgresses
                .Where(u => u.UserId == userId)
                .Include(u => u.Lesson)
                .ToListAsync();
            return Ok(list);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userProgres = await _db.UserProgresses.FindAsync(id);
            if (userProgres is null) return NotFound();

            _db.UserProgresses.Remove(userProgres);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
