using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer;
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

        [HttpGet("adminPanel/daysoff/pending")]
        public async Task<IActionResult> GetPendingDaysOff()
        {
            var daysoff = await _context.DaysOff.Where(d => d.AcceptStatus == "pending").Select(d => new { d.StartDate, d.EndDate, d.Employee.FirstName, d.Employee.MiddleName, d.Employee.LastName, d.AcceptStatus, d.ID }).ToListAsync();
            return Ok(daysoff);
        }

        [HttpGet("adminPanel/daysoff/accepted")]
        public async Task<IActionResult> AdminGetAcceptedDaysOff()
        {
            var daysoff = await _context.DaysOff.Where(d => d.AcceptStatus == "accepted").Select(d => new { d.StartDate, d.EndDate, d.Employee.FirstName, d.Employee.MiddleName, d.Employee.LastName, d.AcceptStatus, d.ID }).ToListAsync();
            return Ok(daysoff);
        }

        [HttpGet("adminPanel/daysoff/rejected")]
        public async Task<IActionResult> GetRejectedDaysOff()
        {
            var daysoff = await _context.DaysOff.Where(d => d.AcceptStatus == "rejected").Select(d => new { d.StartDate, d.EndDate, d.Employee.FirstName, d.Employee.MiddleName, d.Employee.LastName, d.AcceptStatus, d.ID }).ToListAsync();
            return Ok(daysoff);
        }

        [HttpPut("adminPanel/daysoff/decide")]
        public async Task<IActionResult> DecideDayOff([FromBody] DayOffDecisionRequest request)
        {
            var dayOff = await _context.DaysOff.FindAsync(request.DayOffID);

            if (dayOff == null)
            {
                return BadRequest(new { message = "Day off request not found."});
            }

            if (dayOff.AcceptStatus == "accepted" && dayOff.AcceptStatus == "rejected")
            {
                return BadRequest(new { message = "This request was already decided."});
            }

            dayOff.AcceptStatus = request.Status;
            await _context.SaveChangesAsync();

            return Ok(new {message="Decision saved."});
        }

        public class DayOffDecisionRequest
        {
            public int DayOffID { get; set; }
            public string Status { get; set; }
        }

        [HttpGet("adminPanel/summary/{emplID}/{date}")]
        public async Task<IActionResult> GetSummary(int emplID, DateOnly date)
        {
            var employee = await _context.Employees.FindAsync(emplID);

            if (employee == null)
            {
                return BadRequest(new { message = "There is no employee with that id." });
            }

            var IsDayOff = await _context.DaysOff.Where(d => d.StartDate <= date && d.EndDate >= date && d.AcceptStatus == "accepted").AnyAsync();

            if (IsDayOff)
            {
                return Ok(new {status="dayOff", message = "Had day off."});
            }

            DateTime startDay = date.ToDateTime(TimeOnly.MinValue);
            DateTime startNextDay = date.ToDateTime(TimeOnly.MaxValue);

            var selectedDay = await _context.WorkSchedule.Where(w => w.EmployeeID == emplID && ((w.WorkStart.Date >= startDay && w.WorkStart < startNextDay) || (w.WorkEnd >= startDay && w.WorkEnd < startNextDay))).Include(w => w.Breaks).FirstOrDefaultAsync();

            if (selectedDay == null)
            {
                return BadRequest(new { message = "No workday in this date." });
            }

            TimeSpan workdayLength = (selectedDay.WorkEnd ?? DateTime.Now) - selectedDay.WorkStart;

            int totalBreaks = selectedDay.Breaks.Count;

            TimeSpan totalBreaksLength = TimeSpan.FromTicks(selectedDay.Breaks.Sum(b => ((b.BreakEnd ?? DateTime.Now) - b.BreakStart).Ticks));

            TimeSpan totalWorkLength = workdayLength - totalBreaksLength;

            return Ok(new { status = "workday", wdLength = workdayLength, allBreaks = totalBreaks, allBLength = totalBreaksLength, finalWDLength = totalWorkLength});
        }

        [HttpGet("adminPanel/dayoffSummary/{choosenDate}")]
        public async Task<IActionResult> GetDayOffSummary(DateOnly choosenDate)
        {
            DateOnly startMonth = new DateOnly(choosenDate.Year, choosenDate.Month, 1);
            DateOnly endMonth = new DateOnly(choosenDate.Year, choosenDate.Month, DateTime.DaysInMonth(choosenDate.Year, choosenDate.Month));

            var employees = await _context.Employees.Include(e => e.DaysOff).ToListAsync();

            var result = employees.Select(e=> new
            {
                e.ID,
                e.FirstName,
                e.MiddleName,
                e.LastName,
                DaysOffCount = e.DaysOff.Where(d=> d.StartDate <= endMonth && d.EndDate >= startMonth && d.AcceptStatus == "accepted").Sum(d=> ((d.EndDate < endMonth ? d.EndDate : endMonth).DayNumber) - ((d.StartDate > startMonth ? d.StartDate : startMonth).DayNumber) + 1 )
            }).ToList();

            
            return Ok(result);
        }
        private bool Day_OffExists(int id)
        {
            return _context.DaysOff.Any(e => e.ID == id);
        }
    }
}
