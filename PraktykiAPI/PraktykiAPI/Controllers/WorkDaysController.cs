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
            bool alreadyStarted = await _context.WorkSchedule.AnyAsync(w => w.EmployeeID == emplID && w.WorkStart == DateTime.Now);

            if (alreadyStarted)
            {
                return BadRequest("Workday already started.");
            }

            WorkDay newWorkDay = new WorkDay()
            {
                WorkStart = DateTime.Now,
                EmployeeID = emplID,
            };

            _context.WorkSchedule.Add(newWorkDay);
            await _context.SaveChangesAsync();

            return Ok("Workday started.");
        }

        [HttpPut("emplPanel/workday/end/{emplID}")]
        public async Task<IActionResult> EndtWorkday(int emplID)
        {
            var workday = await _context.WorkSchedule.FirstOrDefaultAsync(w => w.EmployeeID == emplID && w.WorkStart == DateTime.Now);

            if (workday == null)
            {
                return BadRequest("Workday wasn't started.");
            }

            if (workday.WorkEnd != null)
            {
                return BadRequest("Workday already ended.");
            }

            workday.WorkEnd = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok("Workday ended.");
        }

        [HttpGet("emplPanel/workday/status/{emplID}")]
        public async Task<IActionResult> getWorkdayStatus(int emplID)
        {
            var workday = await _context.WorkSchedule.FirstOrDefaultAsync(w => w.EmployeeID == emplID && w.WorkStart == DateTime.Now);

            if (workday == null)
            {
                return Ok("notStarted");
            }

            if (workday.WorkEnd == null)
            {
                return Ok("working");
            }

            return Ok("ended");
        }
        private bool WorkDayExists(int id)
        {
            return _context.WorkSchedule.Any(e => e.ID == id);
        }
    }
}
