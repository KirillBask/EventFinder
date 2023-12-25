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

        [HttpPost("CreateEvent")]
        public async Task<ActionResult<Event>> CreateEvent([FromBody] Event newEvent)
        {
            try
            {
                var result = await _eventService.CreateEvent(newEvent);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating event: {ex.Message}");
            }
        }

        [HttpGet("GetEventById/{id}")]
        public async Task<ActionResult<Event>> GetEventById(Guid id)
        {
            try
            {
                var result = await _eventService.GetEventById(id);
                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error getting event: {ex.Message}");
            }
        }

        [HttpGet("GetAllEvents")]
        public async Task<ActionResult<List<Event>>> GetAllEvents()
        {
            try
            {
                var result = await _eventService.GetAllEvents();
                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error getting all events, error: {ex.Message}");
            }
        }

        [HttpPut("UpdateEvent")]
        public async Task<ActionResult<Event>> UpdateEvent(Guid id, Event eventToUpdate)
        {
            try
            {
                var result = await _eventService.UpdateEvent(id, eventToUpdate);
                if (result == null)
                {
                    return NotFound();
                }

                return Ok(eventToUpdate);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating event: {ex.Message}");
            }
        }

        [HttpDelete("DeleteEvent")]
        public async Task<ActionResult<Event>> DeleteEvent(Guid id)
        {
            try
            {
                var result = await _eventService.DeleteEvent(id);
                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting event: {ex.Message}");
            }
        }
    }
}
