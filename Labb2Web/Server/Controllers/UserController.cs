using Labb2Web.Server.DAL.Models;
using Labb2Web.Server.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Labb2Web.Server.Interfaces;

namespace Labb2_API.Controllers;
[ApiController]
[Route("api/[Controller]")]
public class UserController : ControllerBase
{
    private readonly IRepository<User> repository;

    public UserController(IRepository<User> repository)
    {
        this.repository = repository;
    }

    // GET /api/User
    [HttpGet]
    public async Task<IEnumerable<UserForList>> GetUsersAsync()
    {
        var items = (await repository.GetAllAsync());
        var users = items.Select(u => new UserForList
        {
            UserId = u.UserId,
            Firstname = u.Firstname,
            Lastname = u.Lastname,
            Email = u.Email,
            PhoneNumber = u.PhoneNumber,
            Adress = u.Adress
        });
        return users;
    }

    // GET /api/User/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUserAsync(int id)
    {
        var item = await repository.GetByIdAsync(id);
        if (item is null)
        {
            return NotFound();
        }
        return item;
    }

    // GET /api/User/GetUsersByCourseIdAsync/{id}
    [Route("[action]/{id}")]
    [HttpGet]
    public async Task<IEnumerable<UserForList>> GetUsersByCourseIdAsync(int id)
    {
        var userList = new List<UserForList>();
        var users = await repository.GetAllByIdAsync(id);
        foreach (var user in users)
        {
            var newuser = new UserForList()
            {
                UserId = user.UserId,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Adress = user.Adress
            };
            userList.Add(newuser);
        }

        return userList;
    }

    // POST /api/User
    [HttpPost]
    public async Task<ActionResult<User>> CreateUserAsync(CreateUser createItem)
    {
        User item = new()
        {
            Firstname = createItem.Firstname,
            Lastname = createItem.Lastname,
            Email = createItem.Email,
            PhoneNumber = createItem.PhoneNumber,
            Adress = createItem.Adress
        };
        await repository.CreateAsync(item);

        return item;
    }

    // DELETE /api/User
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUserAsync(int id)
    {
        await repository.DeleteAsync(id);
        return NoContent();
    }

    // PUT /api/User
    [HttpPut("{id}")]
    public async Task<IActionResult> EditUserAsync(int id, CreateUser newUser)
    {
        var oldUser = await repository.GetByIdAsync(id);

        if (oldUser == null)
        {
            return NotFound();
        }

        oldUser.Firstname = newUser.Firstname;
        oldUser.Lastname = newUser.Lastname;
        oldUser.Email = newUser.Email;
        oldUser.PhoneNumber = newUser.PhoneNumber;
        oldUser.Adress = newUser.Adress;

        await repository.UpdateAsync(oldUser);
        return NoContent();
    }

    // POST /api/User/CreateManyToMany
    [Route("[action]")]
    [HttpPost]
    public async Task CreateManyToMany(CourseUser courseUser)
    {
        await repository.CreateManyToMany(courseUser);
    }
}