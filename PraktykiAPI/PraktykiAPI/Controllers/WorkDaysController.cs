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

        [HttpGet("workday/admin/{id}/{date}")]
        public async Task<IActionResult> GetWorkdayLength(DateOnly date, int id)
        {
            var workday = await _context.Work_Timetable.Where(w => _context.Employees.Any(e => e.ID == w.Employee_Id && w.Employee_Id == id && w.Date == date)).Select(w => new {Duration = w.Work_End_Hour - w.Work_Start_Hour}).ToListAsync();

            return Ok(workday);
        }

        [HttpGet("workday/admin/{id}/{date_start}/{date_end}")]
        public async Task<IActionResult> GetWorkdayLengthWeek(DateOnly date_start, DateOnly date_end, int id)
        {
            var workdays = await _context.Work_Timetable.Where(w => _context.Employees.Any(e=> e.ID == w.Employee_Id && w.Date >= date_start && w.Date <= date_end && w.Employee_Id == id)).Select(w=> new { w.Date, Duration = w.Work_End_Hour - w.Work_Start_Hour }).ToListAsync();

            double WorkDuration = 0;

            for (var day = date_start; day <= date_end; day = day.AddDays(1))
            {
                var workday = workdays.FirstOrDefault(w => w.Date == day);
                if(workday != null)
                {
                    WorkDuration += workday.Duration.TotalHours;
                }
            }

            return Ok(WorkDuration);
        }

        [HttpGet("workday/admin/{id}/{month}")]
        public async Task<IActionResult> GetWorkdayLengthMonth(int month, int id)
        {
            var workdays = await _context.Work_Timetable.Where(w=> _context.Employees.Any(e=> e.ID == w.Employee_Id && w.Date.Month == month && w.Employee_Id == id)).Select(w => new {w.Date, Duration = w.Work_End_Hour - w.Work_Start_Hour}).ToListAsync();

            double workDuration = 0;
            DateOnly start_day = new DateOnly(DateTime.Now.Year, month, 1);
            DateOnly end_day = new DateOnly(DateTime.Now.Year, month, DateTime.DaysInMonth(DateTime.Now.Year, month));
            for (var day = start_day; day <= end_day; day = day.AddDays(1))
            {
                var workday = workdays.FirstOrDefault(w => w.Date == day);
                if (workday != null)
                {
                    workDuration += workday.Duration.TotalHours;
                }
            }

            return Ok(workDuration);
        }

        private bool WorkDayExists(int id)
        {
            return _context.Work_Timetable.Any(e => e.ID == id);
        }
    }
}
