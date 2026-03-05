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

        //zwraca wszystkie dni wolne ZMIENIC NA MEISIAC
        [HttpGet("admin/dayoff")]
        public async Task<IActionResult> AdminGetDays_Off()
        {
            var daysoff = _context.Days_Off.ToListAsync();

            return Ok(daysoff);
        }

        [HttpGet("admin/dayoff/{status}")]
        public async Task<IActionResult> AdminGetDays_OffStat(string status)
        {
            var daysoff = _context.Days_Off.Where(d => d.Status == status).ToListAsync();

            return Ok(daysoff);
        }

        [HttpGet("dayoff")]
        public async Task<IActionResult> GetDays_Off()
        {
            var daysoff = _context.Days_Off.Where(d=> d.Status == "Accepted").ToListAsync();

            return Ok(daysoff);
        }

        //Zmienia status dla dnia wolnego o
        [HttpPut("admin/dayoff/{id}/{status}")]
        public async Task<IActionResult> ChangeValueDays_Off(int id, string status)
        {
            var daysoff = await _context.Days_Off.FindAsync(id);

            if (daysoff == null)
            {
                return NotFound();
            }

            daysoff.Status = status;

            return Ok(daysoff);
        }

        // GET: api/Day_Off
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Day_Off>>> GetDays_Off()
        //{
        //    return await _context.Days_Off.ToListAsync();
        //}

        // GET: api/Day_Off/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Day_Off>> GetDay_Off(int id)
        //{
        //    var day_Off = await _context.Days_Off.FindAsync(id);

        //    if (day_Off == null)
        //    {
        //        return NotFound();
        //    }

        //    return day_Off;
        //}

        // PUT: api/Day_Off/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutDay_Off(int id, Day_Off day_Off)
        //{
        //    if (id != day_Off.ID)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(day_Off).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!Day_OffExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Day_Off
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Day_Off>> PostDay_Off(Day_Off day_Off)
        //{
        //    _context.Days_Off.Add(day_Off);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetDay_Off", new { id = day_Off.ID }, day_Off);
        //}

        //// DELETE: api/Day_Off/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteDay_Off(int id)
        //{
        //    var day_Off = await _context.Days_Off.FindAsync(id);
        //    if (day_Off == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Days_Off.Remove(day_Off);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool Day_OffExists(int id)
        {
            return _context.Days_Off.Any(e => e.ID == id);
        }
    }
}
