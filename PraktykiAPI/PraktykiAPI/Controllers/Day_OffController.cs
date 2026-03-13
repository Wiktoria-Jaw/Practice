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

        //zwraca wszsytkie dni wolne, narazie bez sortowania na zatwierdzone i niezatwierdzone
        [HttpGet("daysoff")]
        public async Task<IActionResult> GetDaysOff()
        {
            var daysoff = await _context.Days_Off.Join(_context.Employees,  d=> d.Employee_ID, e => e.ID, (d,e) => new { d.Start_Date, d.End_Date, e.Name, e.Surname }).ToListAsync();
            return Ok(daysoff);
        }

        private bool Day_OffExists(int id)
        {
            return _context.Days_Off.Any(e => e.ID == id);
        }
    }
}
