using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookWebDotNet.Domain.Dtos;
using BookWebDotNet.Domain.Entity;
using BookWebDotNet.Service;

namespace BookWebDotNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsersAsync()
        {
            var users = await _service.GetAllUsersAsync();

            return Ok(users);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUserAsync([FromBody] CreateUserDto dto)
        {
            //TODO: check if email exists
            var user = await _service.CreateUserAsync(dto);

            return CreatedAtAction(nameof(GetAllUsersAsync), new {id = user.UserId}, user);
        }

        [HttpGet("/api/[controller]/{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(Guid id)
        {
            var user = await _service.GetUserAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("/api/[controller]/{email}")]
        public async Task<ActionResult<UserDto>> GetUserById(string email)
        {
            var user = await _service.GetUserAsync(email);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPut]
        public async Task<ActionResult<UserDto>> UpdateUser([FromBody] UserDto dto)
        {
            var user = await _service.UpdateUserAsync(dto);

            return Ok(user);
        }

        [HttpDelete("/api/[controller]/{id}")]
        public async Task<ActionResult> DeleteUserById(Guid id)
        {
            await _service.DeleteUserAsync(id);

            return Ok();
        }

    }
}
