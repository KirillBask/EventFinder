using EventFinder.Data;
using EventFinder.Models;
using EventFinder.Services.Interfaces;
using MongoDB.Driver;

namespace EventFinder.Services
{
    public class EventService : IEventService
    {
        private readonly IMongoCollection<Event> _eventCollection;

        public EventService(DatabaseContext databaseContext)
        {
            _eventCollection = databaseContext.Events;
        }

        public Event CreateEvent(Event newEvent)
        {
            try
            {
                newEvent.Id = Guid.NewGuid();
                newEvent.CreatedDate = DateTime.UtcNow;

                _eventCollection.InsertOne(newEvent);

                return newEvent;
            }
            catch (Exception ex)
            {
                throw new Exception($"Coudn't make new event, error : {ex}");            
            }
        }
    }
}
