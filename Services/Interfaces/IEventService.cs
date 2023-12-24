using EventFinder.Models;

namespace EventFinder.Services.Interfaces
{
    public interface IEventService
    {
        public Event CreateEvent(Event newEvent);
    }
}
