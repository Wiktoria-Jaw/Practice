using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PraktykiAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PraktykiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkDaysController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WorkDaysController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("emplPanel/workday/start/{emplID}")]
        public async Task<IActionResult> StartWorkday(int emplID)
        {
            var lastWorkday = await _context.WorkSchedule.Where(w => w.EmployeeID == emplID).OrderByDescending(w=>w.WorkStart).FirstOrDefaultAsync();

            var settings = await _context.WorkSettings.FirstOrDefaultAsync();

            TimeSpan autoEndWorkdayLength = TimeSpan.FromMinutes(settings?.AutoEndWorkdayLengthInMinutes ?? 960);

            if (lastWorkday != null && lastWorkday.WorkEnd == null)
            {
                var duration = DateTime.Now - lastWorkday.WorkStart;
                if (duration > autoEndWorkdayLength)
                {
                    lastWorkday.WorkEnd = DateTime.Now;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return BadRequest(new { status = "error", message="Workday already started."});
                }
            }

            TimeSpan breakBetweenWorkdaysLength = TimeSpan.FromMinutes(settings?.MinBreakBetweenWorkdaysInMinutes ?? 480);

            if (lastWorkday != null && lastWorkday.WorkEnd != null)
            {
                var timeSinceEnd = DateTime.Now - lastWorkday.WorkEnd.Value;
                if(timeSinceEnd < breakBetweenWorkdaysLength)
                {
                    return BadRequest(new { status = "error", message = "You must wait before starting a new workday." });
                }
            }

            WorkDay newWorkDay = new WorkDay()
            {
                WorkStart = DateTime.Now,
                EmployeeID = emplID,
            };

            _context.WorkSchedule.Add(newWorkDay);
            await _context.SaveChangesAsync();

            return Ok(new { status = "working", message = "Workday started." });
        }

        [HttpPut("emplPanel/workday/end/{emplID}")]
        public async Task<IActionResult> EndWorkday(int emplID)
        {
            var workday = await _context.WorkSchedule.Where(w => w.EmployeeID == emplID && w.WorkEnd == null).OrderByDescending(w => w.WorkStart).FirstOrDefaultAsync();

            var settings = await _context.WorkSettings.FirstOrDefaultAsync();

            if (workday == null)
            {
                return BadRequest(new { status = "error", message = "Workday wasn't started." });
            }

            TimeSpan minWorkdayLength = TimeSpan.FromMinutes(settings?.MinWorkdayLengthInMinutes ?? 10);
            var duration = DateTime.Now - workday.WorkStart;

            if (duration < minWorkdayLength)
            {
                return BadRequest(new { status = "error", message = "Workday too short." });
            }

            workday.WorkEnd = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok(new { status = "finished", startTime = workday.WorkStart, endTime = workday.WorkEnd, message = "Workday ended." });
        }

        [HttpGet("emplPanel/workday/status/{emplID}")]
        public async Task<IActionResult> getWorkdayStatus(int emplID)
        {
            var workday = await _context.WorkSchedule.Where(w => w.EmployeeID == emplID).OrderByDescending(w => w.WorkStart).FirstOrDefaultAsync();

            if (workday == null)
            {
                return Ok( new { status = "notStarted", startTime = (DateTime?)null, endTime = (DateTime?)null });
            }

            return Ok(new { status = workday.WorkEnd == null ? "working" : "finished", startTime = workday.WorkStart, endTime = workday.WorkEnd });
        }
        private bool WorkDayExists(int id)
        {
            return _context.WorkSchedule.Any(e => e.ID == id);
        }
    }
}
