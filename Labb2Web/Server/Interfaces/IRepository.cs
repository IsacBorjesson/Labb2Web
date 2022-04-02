using Labb2Web.Server.DAL.Models;

namespace Labb2Web.Server.Interfaces;

public interface IRepository<T>
{
    Task<T> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetAllByIdAsync(Guid id);
    Task CreateAsync(T item);
    Task UpdateAsync(T item);
    Task DeleteAsync(Guid id);
    Task CreateManyToMany(CourseUser item);
}

