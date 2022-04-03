using Labb2Web.Server.DAL.Data;
using Labb2Web.Server.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Labb2Web.Server.Interfaces;

namespace Labb2Web.Server.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(User item)
        {
            await _context.Users.AddAsync(item);
            _context.SaveChanges();
        }

        public async Task CreateManyToMany(CourseUser item)
        {
            var course = await _context.Courses.Include(x => x.Users).FirstOrDefaultAsync(x => x.CourseId == item.CourseId);
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == item.UserId);
            course.Users.Add(user);
            _context.SaveChanges();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
            if (user == null)
            {
                return;
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.Where(_ => true).ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllByIdAsync(int id)
        {
            var course = await _context.Courses.Include(c => c.Users).FirstOrDefaultAsync(c => c.CourseId == id);
            var users1 = course.Users;
            
            return users1;
        }

        public async Task<User> GetByIdAsync(int id)
        {

            User? user = await _context.Users.FindAsync(id);
            if(user == null)
            {
                throw new Exception();
            }
            return user;
        }

        public async Task UpdateAsync(User item)
        {
            if(item == null)
            {
                return;
            }
            _context.Users.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
