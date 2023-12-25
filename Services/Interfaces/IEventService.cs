using EventFinder.Models;

namespace EventFinder.Services.Interfaces
{
    public interface IEventService
    {
        public Task<Event> CreateEvent(Event newEvent);
        public Task<Event> GetEventById(Guid id);
        public Task<List<Event>> GetAllEvents();
        public Task<Event> UpdateEvent(Guid id, Event evenToUpdate);
        public Task<Event> DeleteEvent(Guid id);
    }
}
