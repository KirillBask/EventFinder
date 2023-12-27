using EventFinder.Models;
using EventFinder.Services.Interfaces;
using MongoDB.Driver;

namespace EventFinder.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _userCollection;
        private readonly IMongoCollection<Event> _eventCollection;
        public UserService(IMongoCollection<User> userCollection, IMongoCollection<Event> eventCollection) 
        {
            _userCollection = userCollection;
            _eventCollection = eventCollection;
        }

        public async Task<Event> AddUserToEvent(Guid userId, Guid eventId)
        {
            try
            {
                var cursor = await _eventCollection.FindAsync(result => result.Id == eventId);
                var userEvent = await cursor.FirstOrDefaultAsync();
                
                if (userEvent != null)
                {
                    userEvent.Participants.Add(userId);
                    return userEvent;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Coudn't add new user to event, error : {ex}");
            }
        }

        public async Task<User> CreateUser(User user)
        {
            try
            {
                user.Id = Guid.NewGuid();

                await _userCollection.InsertOneAsync(user);

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"Coudn't make new user, error : {ex}");
            }
        }

        public async Task<User> DeleteUser(Guid id)
        {
            try
            {
                var filter = Builders<User>.Filter.Eq(x => x.Id, id);

                var result = await _userCollection.Find(filter).FirstOrDefaultAsync();

                if (result != null)
                {
                    await _userCollection.DeleteOneAsync(filter);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't delete an user with this id, error: {ex}");
            }
        }

        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                var cursor = await _userCollection.FindAsync(result => result != null);
                var result = cursor.ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error looking for all user ids, error: {ex}");
            }
        }

        public async Task<User> GetUserById(Guid id)
        {
            try
            {
                var cursor = await _userCollection.FindAsync(result => result.Id == id);

                var result = await cursor.FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't find an event with this id, error: {ex}");
            }
        }

        public async Task<User> UpdateUser(Guid id, User user)
        {
            try
            {
                var filter = Builders<User>.Filter.Eq(x => x.Id, id);

                var updateDefinition = Builders<User>.Update
                    .Set(x => x.Name, user.Name);

                var result = await _userCollection.FindOneAndUpdateAsync(filter, updateDefinition, new FindOneAndUpdateOptions<User>
                {
                    ReturnDocument = ReturnDocument.After
                });

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't update an event with this id, error: {ex}");
            }
        }
    }
}
