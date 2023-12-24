using EventFinder.Data;
using EventFinder.Models;
using EventFinder.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace EventFinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost]
        public IActionResult CreateEvent([FromBody] Event newEvent)
        {
            var result = _eventService.CreateEvent(newEvent);
            return Ok(result);
        }
    }
}
