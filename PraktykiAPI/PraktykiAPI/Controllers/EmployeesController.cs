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

        [HttpGet("employee/allemployee")]
        public async Task<IActionResult> AdminGetAllEmployeeData(
            )
        {
            var employees = await _context.Employees.Select(e=> new { e.FirstName, e.MiddleName, e.LastName, e.ID}).ToListAsync();

            return Ok(employees);
        }
        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.ID == id);
        }
    }
}
