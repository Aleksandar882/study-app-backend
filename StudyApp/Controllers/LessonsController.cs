using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyApp.Data;
using StudyApp.DTO;
using StudyApp.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace StudyApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LessonsController : ControllerBase
    {
        private readonly StudyAppDbContext _db;
        public LessonsController(StudyAppDbContext db)
        {
            _db = db;
        }

        [SwaggerOperation(
         Summary = "Gets all lessons",
         Description = "Returns a list of all lessons in the database."
        )]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _db.Lessons.Include(l => l.Course).ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var lesson = await _db.Lessons.Include(l => l.Course).FirstOrDefaultAsync(l => l.Id == id);
            return lesson is null ? NotFound() : Ok(lesson);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateLessonDto dto)
        {
            var lesson = new Lesson
            {
                Title = dto.Title,
                Content = dto.Content,
                Order = dto.Order,
                CourseId = dto.CourseId
            };

            _db.Lessons.Add(lesson);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = lesson.Id }, lesson);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Lesson input)
        {
            var lesson = await _db.Lessons.FindAsync(id);
            if (lesson is null) return NotFound();

            lesson.Title = input.Title;
            lesson.Content = input.Content;
            lesson.CourseId = input.CourseId;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var lesson = await _db.Lessons.FindAsync(id);
            if (lesson is null) return NotFound();

            _db.Lessons.Remove(lesson);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
