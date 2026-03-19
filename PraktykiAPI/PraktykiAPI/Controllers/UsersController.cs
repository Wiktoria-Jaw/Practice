using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PraktykiAPI.Models;

namespace PraktykiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<User> _hasher;

        public UsersController(AppDbContext context)
        {
            _context = context;
            _hasher = new PasswordHasher<User>();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if(request == null || string.IsNullOrEmpty(request.Login) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { message = "Login and password are required." });
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == request.Login);
            if (user == null)
            {
                return BadRequest(new { message = "Incorrect login or password." });
            }

            var result = _hasher.VerifyHashedPassword(user, user.Password, request.Password);
            if(result == PasswordVerificationResult.Failed)
            {
                return BadRequest(new { message = "Incorrect login or password." });
            }

            if (user.IsActive == 0)
            {
                return BadRequest(new {message = "This user isn't an active user."});
            }

            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.ID == user.EmployeeID);

            if (employee == null)
            {
                return BadRequest(new { message = "Employee not found." });
            }

            user.IsLogIn = 1;
            await _context.SaveChangesAsync();

            return Ok(new { id =user.EmployeeID, firstName = employee.FirstName, middleName = employee.MiddleName, lastName = employee.LastName, permission=user.Permission, login=user.Login});
        }
        public class LoginRequest
        {
            public string Login { get; set; }
            public string Password { get; set; }
        }

        [HttpPut("logout/{emplID}")]
        public async Task<IActionResult> Logout(int emplID)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.EmployeeID == emplID);

            if(user == null)
            {
                return BadRequest(new {message="User not found."});
            }

            user.IsLogIn = 0;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Sucessfully log out." });
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }
    }
}
