#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fragments.Data.Context;
using Fragments.Data.Entities;

namespace Fragments.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly FragmentsContext _context;

        public UsersController(FragmentsContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (UserExists(user.Email))
            {
                return BadRequest();
            }
            _context.users.Add(user);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool UserExists(string email)
        {
            return _context.users.Any(e => e.Email == email);
        }
    }
}
