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
    public class BreaksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BreaksController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("emplPanel/workday/break/start/{emplID}")]
        public async Task<IActionResult> StartBreak(int emplID)
        {
            var workday = await _context.WorkSchedule.Where(w => w.EmployeeID == emplID && w.WorkEnd == null).OrderByDescending(w => w.WorkStart).FirstOrDefaultAsync();

            if (workday == null)
            {
                return BadRequest(new { status = "error", message = "Workday not started." });
            }

            var timeFromStartingWorkday = DateTime.Now - workday.WorkStart;

            if (timeFromStartingWorkday.TotalMinutes < 10)
            {
                return BadRequest(new { status = "error", message = "Break is not allowed yet." });
            }

            bool breakRunning = await _context.Breaks.AnyAsync(b => b.WorkDayID == workday.ID && b.BreakEnd == null);

            if (breakRunning)
            {
                return BadRequest(new { status = "error", message = "Break already started." });
            }

            Break newBreak = new Break()
            {
                WorkDayID = workday.ID,
                BreakStart = DateTime.Now,
            };

            _context.Breaks.Add(newBreak);
            await _context.SaveChangesAsync();

            return Ok(new { status = "finished", startTime = newBreak.BreakStart, endTime = newBreak.BreakEnd, message = "Break started" });
        }

        [HttpPut("emplPanel/workday/break/end/{emplID}")]
        public async Task<IActionResult> EndBreak(int emplID)
        {
            var workday = await _context.WorkSchedule.Where(w => w.EmployeeID == emplID && w.WorkEnd == null).OrderByDescending(w => w.WorkStart).FirstOrDefaultAsync();

            if (workday == null)
            {
                return BadRequest(new { status = "error", message = "Workday not started." });
            }

            var breakCur = await _context.Breaks.Where(b => b.WorkDayID == workday.ID && b.BreakEnd == null).OrderByDescending(b=> b.BreakStart).FirstOrDefaultAsync();

            if (breakCur == null)
            {
                return BadRequest(new { status = "error", message = "No active break" });
            }

            var breakDuration = DateTime.Now - breakCur.BreakStart;

            if (breakDuration.TotalMinutes < 5)
            {
                return BadRequest(new { status = "error", message = "Break is too short." });
            }

            breakCur.BreakEnd = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok(new { status = "finished", startTime = breakCur.BreakStart, endTime = breakCur.BreakEnd, message = "Break ended." });
        }

        [HttpGet("emplPanel/workday/break/status/{emplID}")]
        public async Task<IActionResult> getBreakStatus(int emplID)
        {
            var workday = await _context.WorkSchedule.Where(w => w.EmployeeID == emplID && w.WorkEnd == null).OrderByDescending(w => w.WorkStart.Date).FirstOrDefaultAsync();

            if (workday == null)
            {
                return Ok(new { status = "notStarted", startTime = (DateTime?)null, endTime = (DateTime?)null });
            }

            var lastBreak = await _context.Breaks.Where(b => b.WorkDayID == workday.ID).OrderByDescending(b => b.BreakStart).FirstOrDefaultAsync();

            if (lastBreak == null)
            {
                return Ok(new {status = "notStarted", startTime = (DateTime?)null, endTime = (DateTime?)null });
            }

            return Ok(new { status = lastBreak.BreakEnd == null ? "onBreak" : "finished", startTime = lastBreak.BreakStart, endTime = lastBreak.BreakEnd });
        }

        private bool BreakExists(int id)
        {
            return _context.Breaks.Any(e => e.ID == id);
        }
    }
}
