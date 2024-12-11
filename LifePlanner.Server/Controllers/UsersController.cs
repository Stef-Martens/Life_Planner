using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LifePlanner.Server.Data;
using LifePlanner.Server.Models;
using LifePlanner.Server.Services.Interfaces;

namespace LifePlanner.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userService.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // GET: api/users/auth0/auth0|5f7e7b7b7b7b7b7b7b7b7b7b7b7b7b7
        [HttpGet("auth0/{auth0Id}")]
        public async Task<ActionResult<User>> GetUserByAuth0Id(string auth0Id)
        {
            var user = await _userService.GetByAuth0Id(auth0Id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            try
            {
                await _userService.Update(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            // Check if auth0Id already exists
            var existingUser = await _userService.GetByAuth0Id(user.Auth0Id);
            if (existingUser != null)
            {
                return Conflict();
            }

            var newUser = await _userService.Add(user);

            if (newUser != null)
            {
                return CreatedAtAction("GetUser", new { id = newUser.Id }, newUser);
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userService.Delete(user.Id);
            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _userService.GetById(id) != null
                ? true
                : false;
        }
    }
}
