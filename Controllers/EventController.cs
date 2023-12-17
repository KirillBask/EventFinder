using EventFinder.Data;
using EventFinder.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace EventFinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IMongoCollection<Event> _eventCollection;

        public EventController(DatabaseContext databaseContext)
        {
            _eventCollection = databaseContext.Events;
        }

        [HttpPost]
        public IActionResult CreateEvent([FromBody] Event newEvent)
        {
            try
            {
                newEvent.Id = Guid.NewGuid();
                newEvent.CreatedDate = DateTime.UtcNow;

                _eventCollection.InsertOne(newEvent);

                return Ok(new { Message = "Event created successfully", EventId = newEvent.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Error creating event", Error = ex.Message });
            }
        }

    }
}
