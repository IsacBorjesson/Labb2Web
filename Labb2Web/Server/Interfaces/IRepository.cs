using Labb2Web.Server.DAL.Models;

namespace Labb2Web.Server.Interfaces;

public interface IRepository<T>
{
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetAllByIdAsync(int id);
    Task CreateAsync(T item);
    Task UpdateAsync(T item);
    Task DeleteAsync(int id);
    Task CreateManyToMany(CourseUser item);
}

