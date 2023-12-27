using EventFinder.Models;
using EventFinder.Services;
using EventFinder.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventFinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("AddUserToEvent")]
        public async Task<ActionResult<User>> AddUserToEvent(Guid userId, Guid eventId)
        {
            try
            {
                var result = await _userService.AddUserToEvent(userId, eventId);
                return Ok(result);
            } 
            catch (Exception ex)
            {
                return BadRequest($"Error creating user: {ex.Message}");
            }
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult<User>> CreateUser([FromBody] User newUser)
        {
            try
            {
                var result = await _userService.CreateUser(newUser);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating user: {ex.Message}");
            }
        }

        [HttpGet("GetUserById/{id}")]
        public async Task<ActionResult<User>> GetUserById(Guid id)
        {
            try
            {
                var result = await _userService.GetUserById(id);
                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error getting user: {ex.Message}");
            }
        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            try
            {
                var result = await _userService.GetAllUsers();
                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error getting all users, error: {ex.Message}");
            }
        }

        [HttpPut("UpdateUser")]
        public async Task<ActionResult<User>> UpdateUser(Guid id, [FromBody] User userToUpdate)
        {
            try
            {
                var result = await _userService.UpdateUser(id, userToUpdate);
                if (result == null)
                {
                    return NotFound();
                }

                return Ok(userToUpdate);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating user: {ex.Message}");
            }
        }

        [HttpDelete("DeleteUser")]
        public async Task<ActionResult<User>> DeleteUser(Guid id)
        {
            try
            {
                var result = await _userService.DeleteUser(id);
                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting user: {ex.Message}");
            }
        }
    }
}
