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

        [HttpGet("employee/{id}")]
        public async Task<IActionResult> AdminGetEmployeeData(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            return Ok(employee);
        }

        //[HttpGet("employee/dayoff")]
        //public async Task<IActionResult> GetDaysOff()
        //{
        //    var employee = await _context.Employees.Where(e => _context.Days_Off.Any(d => d.Employee_ID == e.ID && d.Status == "Accepted")).Select(e => new {e.Name, e.Surname}).ToListAsync();

        //    return Ok(employee);
        //}

        //[HttpGet("admin/employee/dayoff/{status}")]
        //public async Task<IActionResult> AdminGetDaysOff(DateOnly date, String status)
        //{
        //    var employee = await _context.Employees.Where(e => _context.Days_Off.Any(d=> d.Employee_ID == e.ID && d.Status == status)).Select(e => new {e.Name, e.Surname}).ToListAsync();

        //    return Ok(employee);
        //}



        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.ID == id);
        }
    }
}
