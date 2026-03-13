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
            DateOnly now = DateOnly.FromDateTime(DateTime.Now);
            bool alreadyStarted = await _context.Work_Timetable.AnyAsync(w=>w.Employee_Id == emplID && w.Date == now);

            if (alreadyStarted)
            {
                return BadRequest("Workday already started.");
            }

            WorkDay newWorkDay = new WorkDay()
            {
                Date = now,
                Employee_Id = emplID,
                Work_Start_Hour = TimeOnly.FromDateTime(DateTime.Now),
            };
            
            _context.Work_Timetable.Add(newWorkDay);
            await _context.SaveChangesAsync();

            return Ok("Workday started.");
        }

        [HttpPut("emplPanel/workday/end/{emplID}")]
        public async Task<IActionResult> EndtWorkday(int emplID)
        {
            DateOnly now = DateOnly.FromDateTime(DateTime.Now);
            var workday = await _context.Work_Timetable.FirstOrDefaultAsync(w => w.Employee_Id == emplID && w.Date == now);

            if (workday == null)
            {
                return BadRequest("Workday wasn't started.");
            }

            if(workday.Work_End_Hour != null)
            {
                return BadRequest("Workday already ended.");
            }

            workday.Work_End_Hour = TimeOnly.FromDateTime(DateTime.Now);

            await _context.SaveChangesAsync();

            return Ok("Workday ended.");
        }

        [HttpGet("emplPanel/workday/status/{emplID}")]
        public async Task<IActionResult> getWorkdayStatus(int emplID)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            var workday = await _context.Work_Timetable.FirstOrDefaultAsync(w => w.Employee_Id == emplID && w.Date == today);

            if (workday == null)
            {
                return Ok("notStarted");
            }

            if (workday.Work_End_Hour == null)
            {
                return Ok("working");
            }

            return Ok("ended");
        }
        private bool WorkDayExists(int id)
        {
            return _context.Work_Timetable.Any(e => e.ID == id);
        }
    }
}
