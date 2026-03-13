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

        ////zwraca wszystkie dni wolne
        //[HttpGet("admin/dayoff")]
        //public async Task<IActionResult> AdminGetDays_Off()
        //{
        //    var daysoff = _context.Days_Off.ToListAsync();

        //    return Ok(daysoff);
        //}

        //[HttpGet("admin/dayoff/{status}")]
        //public async Task<IActionResult> AdminGetDays_OffStat(string status)
        //{
        //    var daysoff = _context.Days_Off.Where(d => d.Status == status).ToListAsync();

        //    return Ok(daysoff);
        //}

        //[HttpGet("dayoff")]
        //public async Task<IActionResult> GetDays_Off()
        //{
        //    var daysoff = _context.Days_Off.Where(d=> d.Status == "Accepted").ToListAsync();

        //    return Ok(daysoff);
        //}

        //[HttpPut("admin/dayoff/{id}/{status}")]
        //public async Task<IActionResult> ChangeValueDays_Off(int id, string status)
        //{
        //    var daysoff = await _context.Days_Off.FindAsync(id);

        //    if (daysoff == null)
        //    {
        //        return NotFound();
        //    }

        //    daysoff.Status = status;

        //    return Ok(daysoff);
        //}

        //1. zwrocic dni wolnego w ciagu dnia/tygodnia/miesiaca dla tego id pracownika

        private bool Day_OffExists(int id)
        {
            return _context.Days_Off.Any(e => e.ID == id);
        }
    }
}
