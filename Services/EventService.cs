using EventFinder.Data;
using EventFinder.Models;
using EventFinder.Services.Interfaces;
using MongoDB.Driver;
using System.Linq;

namespace EventFinder.Services
{
    public class EventService : IEventService
    {
        private readonly IMongoCollection<Event> _eventCollection;

        public EventService(DatabaseContext databaseContext)
        {
            _eventCollection = databaseContext.Events;
        }

        public async Task<Event> CreateEvent(Event newEvent)
        {
            try
            {
                newEvent.Id = Guid.NewGuid();
                newEvent.CreatedDate = DateTime.UtcNow;

                await _eventCollection.InsertOneAsync(newEvent);

                return newEvent;
            }
            catch (Exception ex)
            {
                throw new Exception($"Coudn't make new event, error : {ex}");            
            }
        }

        public async Task<Event> DeleteEvent(Guid id)
        {
            try
            {
                var filter = Builders<Event>.Filter.Eq(x => x.Id, id);

                var result = await _eventCollection.Find(filter).FirstOrDefaultAsync();

                if (result != null)
                {
                    await _eventCollection.DeleteOneAsync(filter);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't delete an event with this id, error: {ex}");
            }
        }

        public async Task<List<Event>> GetAllEvents()
        {
            try
            {
                var cursor = await _eventCollection.FindAsync(result => result != null);
                var result = cursor.ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error looking for all event ids, error: {ex}");
            }
        }

        public async Task<Event> GetEventById(Guid id)
        {
            try
            {
                var cursor = await _eventCollection.FindAsync(result => result.Id == id);

                var result = await cursor.FirstOrDefaultAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't find an event with this id, error: {ex}");
            }
        }

        public async Task<Event> UpdateEvent(Guid id, Event eventToUpdate)
        {
            try
            {
                var filter = Builders<Event>.Filter.Eq(x => x.Id, id);

                var updateDefinition = Builders<Event>.Update
                    .Set(x => x.EventOrganiser, eventToUpdate.EventOrganiser)
                    .Set(x => x.EventName, eventToUpdate.EventName)
                    .Set(x => x.EventDescription, eventToUpdate.EventDescription)
                    .Set(x => x.EventType, eventToUpdate.EventType)
                    .Set(x => x.Photos, eventToUpdate.Photos)
                    .Set(x => x.CreatedDate, eventToUpdate.CreatedDate)
                    .Set(x => x.UpdatedDate, DateTime.UtcNow)
                    .Set(x => x.EventStart, eventToUpdate.EventStart)
                    .Set(x => x.EventEnd, eventToUpdate.EventEnd)
                    .Set(x => x.Participants, eventToUpdate.Participants);

                var result = await _eventCollection.FindOneAndUpdateAsync(filter, updateDefinition, new FindOneAndUpdateOptions<Event>
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
