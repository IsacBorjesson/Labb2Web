using Labb2Web.Server.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Labb2Web.Server.DAL.Data
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // For sample purposes seed both with the same password.
                // Password is set with the following:
                // dotnet user-secrets set SeedUserPW <pw>
                // The admin user can do anything

               

                SeedDB(context);
            }
        }

        private static void SeedDB(ApplicationDbContext context)
        {
            if (context.Courses.Any())
            {
                return;   // DB has been seeded
            }

            var user1 = new User() {
                Firstname = "Isac",
                Lastname = "Börjesson",
                Email = "isacborje@gmail.com",
                PhoneNumber = "0720195955",
                Adress = "Rådhusgatan 7"
            };
            var user2 = new User()
            {
                Firstname = "Tilde",
                Lastname = "Skrealid",
                Email = "tildeskrea@gmail.com",
                PhoneNumber = "0720195956",
                Adress = "Rådhusgatan 6"
            };

            context.AddRange(
            new Course
            {
                Name = "Matte",
                Description = "Man räknar",
                Length = "2 Månader",
                Level = CourseEnum.Level.Medium,
                Status = CourseEnum.Status.Active,
                Users = new List<User> { user1 }
            },
            new Course
            {
                Name = "Svenska",
                Description = "Man räknar inte",
                Length = "3 Månader",
                Level = CourseEnum.Level.Hard,
                Status = CourseEnum.Status.Active,
                Users = new List<User> { user1, user2 }
            });

            context.SaveChanges();
        }
    }
}
