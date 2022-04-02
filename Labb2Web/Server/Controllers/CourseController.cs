using Labb2Web.Server.DAL.Models;
using Labb2Web.Server.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Labb2Web.Server.Interfaces;

namespace Labb2Web.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly IRepository<Course> repository;

        public CourseController(IRepository<Course> repository)
        {
            this.repository = repository;
        }

        // GET /api/Course
        [HttpGet]
        public async Task<IEnumerable<CourseForList>> GetCoursesAsync()
        {
            var items = await repository.GetAllAsync();
            var courses = items.Select(c => new CourseForList
            {
                CourseId = c.CourseId,
                Name = c.Name,
                Description = c.Description,
                Length = c.Length,
                Level = c.Level,
                Status = c.Status
            });

            return courses;
        }

        // GET /api/Course/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourseAsync(Guid id)
        {
            var item = await repository.GetByIdAsync(id);
            if (item is null)
            {
                return NotFound();
            }
            return item;
        }

        // Get /api/Course/GetCoursesByUserIdAsync/{id} 
        [Route("[action]/{id}")]
        [HttpGet]
        public async Task<IEnumerable<CreateCourse>> GetCoursesByUserIdAsync(Guid id)
        {
            var courseList = new List<CreateCourse>();
            var courses = await repository.GetAllByIdAsync(id);
            foreach (var course in courses)
            {
                var newuser = new CreateCourse()
                {
                    Name = course.Name,
                    Description = course.Description,
                    Length = course.Length,
                    Level = course.Level,
                    Status = course.Status,
                };
                courseList.Add(newuser);
            }

            return courseList;
        }

        // POST /api/Course
        [HttpPost]
        public async Task<ActionResult<Course>> CreateCourseAsync(CreateCourse createItem)
        {
            Course item = new()
            {
                CourseId = Guid.NewGuid(),
                Name = createItem.Name,
                Description = createItem.Description,
                Length = createItem.Length,
                Level = createItem.Level,
                Status = createItem.Status
            };
            await repository.CreateAsync(item);

            return item;
        }

        // Delete /api/Course
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCourseAsync(Guid id)
        {
            await repository.DeleteAsync(id);
            return NoContent();
        }

        // Put /api/Course
        [HttpPut("{id}")]
        public async Task<IActionResult> EditCourseAsync(Guid id, CourseForList newCourse)
        {
            var oldCourse = await repository.GetByIdAsync(id);

            if(oldCourse == null)
            {
                return NotFound();
            }

            oldCourse.Name = newCourse.Name;
            oldCourse.Description = newCourse.Description;
            oldCourse.Length = newCourse.Length;
            oldCourse.Level = newCourse.Level;
            oldCourse.Status = newCourse.Status;

            await repository.UpdateAsync(oldCourse);
            return NoContent();
        }

        // Post /api/Course/CreateManyToMany
        [Route("[action]")]
        [HttpPost]
        public async Task CreateManyToMany(CourseUser courseUser)
        {
            await repository.CreateManyToMany(courseUser);
        }
    }
}
