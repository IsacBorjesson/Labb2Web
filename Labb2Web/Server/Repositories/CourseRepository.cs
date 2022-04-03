using Labb2Web.Server.DAL.Data;
using Labb2Web.Server.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Labb2Web.Server.Interfaces;

namespace Labb2Web.Server.Repositories
{
    public class CourseRepository : IRepository<Course>
    {
        private readonly ApplicationDbContext _context;
        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Course item)
        {
            await _context.Courses.AddAsync(item);
            _context.SaveChanges();
        }

        public async Task CreateManyToMany(CourseUser item)
        {
            var user = await _context.Users.Include(x => x.Courses).FirstOrDefaultAsync(x => x.UserId == item.UserId);
            var course = await _context.Courses.FirstOrDefaultAsync(x => x.CourseId == item.CourseId);
            user.Courses.Add(course);
            _context.SaveChanges();
        }

        public async Task DeleteAsync(int id)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(x => x.CourseId == id);
            if (course == null)
            {
                return;
            }
            _context.Courses.Remove(course);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetAllByIdAsync(int id)
        {
            var user = await _context.Users.Include(c => c.Courses).FirstOrDefaultAsync(c => c.UserId == id);
            var courses = user.Courses;
            return courses;
        }

        public async Task<Course> GetByIdAsync(int id)
        {
            Course? course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                throw new Exception();
            }
            return course;
        }

        public async Task UpdateAsync(Course item)
        {
            if (item == null)
            {
                return;
            }
            _context.Courses.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
