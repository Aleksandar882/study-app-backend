using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudyApp.Data;
using StudyApp.DTO;
using StudyApp.Models;

namespace StudyApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly StudyAppDbContext _db;
        public CoursesController(StudyAppDbContext db) 
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _db.Courses.Include(c => c.Lessons.OrderBy(l => l.Order)).ToListAsync());
        }
            

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var course = await _db.Courses.Include(c => c.Lessons.OrderBy(l => l.Order)).FirstOrDefaultAsync(c => c.Id == id);
            return course is null ? NotFound() : Ok(course);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseDto dto)
        {
            var course = new Course
            {
                Title = dto.Title,
                Description = dto.Description
            };

            _db.Courses.Add(course);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = course.Id }, course);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Course input)
        {
            var course = await _db.Courses.FindAsync(id);
            if (course is null) return NotFound();

            course.Title = input.Title;
            course.Description = input.Description;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _db.Courses.FindAsync(id);
            if (course is null) return NotFound();

            _db.Courses.Remove(course);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
