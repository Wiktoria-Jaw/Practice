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

        //[HttpPost("emplPanel/workday/break/start/{emplID}")]
        //public async Task<IActionResult> StartBreak(int emplID)
        //{
        //    DateOnly nowDate = DateOnly.FromDateTime(DateTime.Now);
        //    TimeOnly nowTime = TimeOnly.FromDateTime(DateTime.Now);
        //    var workday = await _context.Work_Timetable.FirstOrDefaultAsync(w => w.Date == nowDate && w.Employee_Id == emplID && w.Work_End_Hour == null);

        //    if(workday == null)
        //    {
        //        return BadRequest("No such workday/Workday already done today.");
        //    }

        //    bool breakRunning = await _context.Break_Timetable.AnyAsync(b => b.WorkDay_Id == workday.ID && b.Break_End_Hour == null);

        //    if (breakRunning)
        //    {   
        //        return BadRequest("Break already started.");
        //    }

        //    Break newBreak = new Break()
        //    {
        //        WorkDay_Id = workday.ID,
        //        Break_Start_Hour = nowTime,
        //    };

        //    _context.Break_Timetable.Add(newBreak);
        //    await _context.SaveChangesAsync();

        //    return Ok("Break started.");
        //}

        //[HttpPut("emplPanel/workday/break/end/{emplID}")]
        //public async Task<IActionResult> EndBreak(int emplID)
        //{
        //    DateOnly nowDate = DateOnly.FromDateTime(DateTime.Now);
        //    TimeOnly nowTime = TimeOnly.FromDateTime(DateTime.Now);
        //    var workday = await _context.Work_Timetable.FirstOrDefaultAsync(w => w.Date == nowDate && w.Employee_Id == emplID && w.Work_End_Hour == null);

        //    if (workday == null)
        //    {
        //        return BadRequest("No such workday/Workday already done today.");
        //    }

        //    var breakCur = await _context.Break_Timetable.FirstOrDefaultAsync(b => b.WorkDay_Id == workday.ID && b.Break_End_Hour == null);

        //    if (breakCur == null)
        //    {
        //        return BadRequest("No active break");
        //    }

        //    breakCur.Break_End_Hour = nowTime;

        //    await _context.SaveChangesAsync();

        //    return Ok("Break ended.");
        //}

        //[HttpGet("emplPanel/workday/break/status/{emplID}")]
        //public async Task<IActionResult> getWorkdayStatus(int emplID)
        //{
        //    DateOnly nowDate = DateOnly.FromDateTime(DateTime.Now);
        //    var workday = await _context.Work_Timetable.FirstOrDefaultAsync(w => w.Date == nowDate && w.Employee_Id == emplID && w.Work_End_Hour == null);

        //    if (workday == null)
        //    {
        //        return Ok("noBreakStarted");
        //    }

        //    var breakCur = await _context.Break_Timetable.FirstOrDefaultAsync(b => b.WorkDay_Id == workday.ID && b.Break_End_Hour == null);

        //    if (breakCur == null)
        //    {
        //        return Ok("noBreakStarted");
        //    }

        //    return Ok("onBreak");
        //}

        //private bool BreakExists(int id)
        //{
        //    return _context.Break_Timetable.Any(e => e.ID == id);
        //}
    }
}
