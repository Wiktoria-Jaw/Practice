using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PraktykiAPI.Models;

namespace PraktykiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Day_OffController : ControllerBase
    {
        private readonly AppDbContext _context;

        public Day_OffController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("emplPanel/daysoff/accepted")]
        public async Task<IActionResult> GetAcceptedDaysOff()
        {
            var daysoff = await _context.DaysOff.Where( d => d.AcceptStatus == "accepted").Select( d => new {d.StartDate, d.EndDate, d.Employee.FirstName, d.Employee.MiddleName, d.Employee.LastName}).ToListAsync();

            return Ok(daysoff);
        }

        [HttpPost("emplPanel/daysoff/declareDaysOff/{emplID}")]
        public async Task<IActionResult> DeclareDayOff(int emplID, [FromBody] DayOffRequest request)
        {
            if(request.StartDate > request.EndDate)
            {
                return Ok(new { message = "StartDate cannot be after EndDate." });
            }

            bool overlaps = await _context.DaysOff.AnyAsync(d => d.EmployeeID == emplID && (request.StartDate >= d.StartDate && request.StartDate <= d.EndDate) || (request.EndDate >= d.StartDate && request.EndDate <= d.EndDate) || (request.StartDate <= d.StartDate && request.EndDate >= d.EndDate));

            if (overlaps)
            {
                return Ok(new { message = "The employee already has day off in this period."});
            }

            var newDayOff = new DayOff
            {
                EmployeeID = emplID,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
            };

            _context.DaysOff.Add(newDayOff);
            await _context.SaveChangesAsync();

            return Ok(new { message="Day off declared sucesfully."});
        }

        public class DayOffRequest
        {
            public DateOnly StartDate { get; set; }
            public DateOnly EndDate { get; set; }
        }

        //[HttpGet("adminPanel/daysoff")]
        //public async Task<IActionResult> GetAllDaysOff()
        //{
        //    var daysoff = await _context.DaysOff.Select(d => new { d.StartDate, d.EndDate, d.Employee.FirstName, d.Employee.MiddleName, d.Employee.LastName }).ToListAsync();
        //    return Ok(daysoff);
        //}

        private bool Day_OffExists(int id)
        {
            return _context.DaysOff.Any(e => e.ID == id);
        }
    }
}
