using shareCalendar_api.Entities;

namespace shareCalendar_api.Repositories;

public interface IUserRepository
{
    Task<User> GetUserAsync(Guid id);
    Task CreateUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(Guid id);
}