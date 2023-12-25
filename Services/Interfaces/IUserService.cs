using EventFinder.Models;

namespace EventFinder.Services.Interfaces
{
    public interface IUserService
    {
        public Task<User> CreateUser(User user);
        public Task<User> UpdateUser(Guid id, User user);
        public Task<User> DeleteUser(Guid id);
        public Task<User> GetUserById(Guid id);
        public Task<List<User>> GetAllUsers();
        public Task<User> AddUserToEvent(Guid userId, Guid eventId);
    }
}
