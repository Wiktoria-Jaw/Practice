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
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }

        //GET: api/Employees
        // Zwraca imie i nazwisko pracownika w danym dniu wolnym (potrzbne do kalendarza)
        [HttpGet("dayoff/{date}")]
        public async Task<IActionResult> GetEmployeeDaysOff(DateOnly date)
        {
            //var employee = await _context.Employees.FromSqlInterpolated($"select Employees.Name, Employees.Surname from Employees left join Days_Off on Days_Off.Employee_ID = Employees.ID where {date} between Days_Off.Start_Date and Days_Off.End_Date ").ToListAsync();

            var employee = await _context.Employees.Where(e => _context.Days_Off.Any(d => d.Employee_ID == e.ID && date >= d.Start_Date && date <= d.End_Date && d.Status == "Pending")).Select(e => new {e.Name, e.Surname}).ToListAsync();

            return Ok(employee);
        }
        //GET: api/Employees
        // Zwraca imie i nazwisko pracownika w danym dniu wolnym (potrzbne do kalendarza dla admina)
        [HttpGet("admin/dayoff/{date}/{status}")]
        public async Task<IActionResult> AdminGetEmployeeDaysOff(DateOnly date, String status)
        {
            //select Employees.Name, Employees.Surname from Employees left join Days_Off on Days_Off.Employee_ID = Employees.ID where '2026-03-03' between Days_Off.Start_Date and Days_Off.End_Date and Days_Off.Status = 'Pending'

            var employee = await _context.Employees.Where(e => _context.Days_Off.Any(d=> d.Employee_ID == e.ID && date >= d.Start_Date && date <= d.End_Date && d.Status == status)).Select(e => new {e.Name, e.Surname}).ToListAsync();

            return Ok(employee);
        }

        // GET: api/Employees
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        //{
        //    return await _context.Employees.ToListAsync();
        //}

        // GET: api/Employees/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Employee>> GetEmployee(int id)
        //{
        //    var employee = await _context.Employees.FindAsync(id);

        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    return employee;
        //}

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutEmployee(int id, Employee employee)
        //{
        //    if (id != employee.ID)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(employee).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!EmployeeExists(id))
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

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        //{
        //    _context.Employees.Add(employee);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetEmployee", new { id = employee.ID }, employee);
        //}

        // DELETE: api/Employees/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteEmployee(int id)
        //{
        //    var employee = await _context.Employees.FindAsync(id);
        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Employees.Remove(employee);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.ID == id);
        }
    }
}
