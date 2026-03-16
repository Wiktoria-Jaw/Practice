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
            var workday = await _context.WorkSchedule.FirstOrDefaultAsync(w => w.WorkStart == DateTime.Now && w.EmployeeID == emplID && w.WorkEnd == null);

            if (workday == null)
            {
                return BadRequest("No such workday/Workday already done today.");
            }

            bool breakRunning = await _context.Breaks.AnyAsync(b => b.WorkDayID == workday.ID && b.BreakEnd == null);

            if (breakRunning)
            {
                return BadRequest("Break already started.");
            }

            Break newBreak = new Break()
            {
                WorkDayID = workday.ID,
                BreakStart = DateTime.Now,
            };

            _context.Breaks.Add(newBreak);
            await _context.SaveChangesAsync();

            return Ok("Break started.");
        }

        [HttpPut("emplPanel/workday/break/end/{emplID}")]
        public async Task<IActionResult> EndBreak(int emplID)
        {
            var workday = await _context.WorkSchedule.FirstOrDefaultAsync(w => w.WorkStart == DateTime.Now && w.EmployeeID == emplID && w.WorkEnd == null);

            if (workday == null)
            {
                return BadRequest("No such workday/Workday already done today.");
            }

            var breakCur = await _context.Breaks.FirstOrDefaultAsync(b => b.WorkDayID == workday.ID && b.BreakEnd == null);

            if (breakCur == null)
            {
                return BadRequest("No active break");
            }

            breakCur.BreakEnd = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok("Break ended.");
        }

        [HttpGet("emplPanel/workday/break/status/{emplID}")]
        public async Task<IActionResult> getBreakStatus(int emplID)
        {
            var workday = await _context.WorkSchedule.Where(w => w.EmployeeID == emplID && w.WorkEnd == null).OrderByDescending(w => w.WorkStart).FirstOrDefaultAsync();

            if (workday == null)
            {
                return Ok(new { status = "noBreakStarted", startTime = (DateTime?)null, endTime = (DateTime?)null });
            }

            var breakCur = await _context.Breaks.Where(b => b.WorkDayID == workday.ID && b.BreakEnd == null).OrderByDescending(b => b.BreakStart).FirstOrDefaultAsync();

            if (breakCur == null)
            {
                return Ok(new {status = "noBreakStarted", startTime = (DateTime?)null, endTime = (DateTime?)null });
            }

            return Ok(new { status = "onBreak", startTime = breakCur.BreakStart, endTime = breakCur.BreakEnd });
        }

        private bool BreakExists(int id)
        {
            return _context.Breaks.Any(e => e.ID == id);
        }
    }
}
