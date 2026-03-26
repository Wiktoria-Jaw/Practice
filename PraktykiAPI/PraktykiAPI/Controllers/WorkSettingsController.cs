using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PraktykiAPI.Models;

namespace PraktykiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkSettingsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WorkSettingsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("adminPanel/workrules")]
        public async Task<IActionResult> GetWorkSettings()
        {
            var rules = await _context.WorkSettings.FirstOrDefaultAsync();

            if (rules == null)
            {
                return BadRequest(new { message = "No workrules in database."});
            }

            return Ok(new { MinWorkdayLength = rules.MinWorkdayLengthInMinutes, AutoEndWorkday = rules.AutoEndWorkdayLengthInMinutes, MinBreakBetweenWorkdays = rules.MinBreakBetweenWorkdaysInMinutes, MinWorkdayLengthForBreak = rules.MinWorkdayLengthForBreakInMinutes, MinBreakLength = rules.MinBreakLengthInMinutes });
        }

        [HttpPut("adminPanel/workrules/update")]
        public async Task<IActionResult> UpdateWorkSettings([FromBody] NewWorkRules workrules)
        {
            var currWorkRules = await _context.WorkSettings.FirstOrDefaultAsync();
            if (currWorkRules == null)
            {
                if (workrules.MinWorkdayLength == null || workrules.AutoEndWorkday == null || workrules.MinBreakBetweenWorkdays == null || workrules.MinWorkdayLengthForBreak == null || workrules.MinBreakLength == null)
                {
                    return BadRequest(new { message = "ALL work settings must be provided if there aren't any."});
                }

                if (workrules.MinWorkdayLength < 0 || workrules.AutoEndWorkday < 0 || workrules.MinBreakBetweenWorkdays < 0 || workrules.MinWorkdayLengthForBreak < 0 || workrules.MinBreakLength < 0)
                {
                    return BadRequest(new { message = "Work settings can't be negative." });
                }

                var ws = new WorkSettings
                {
                    MinWorkdayLengthInMinutes = workrules.MinWorkdayLength,
                    AutoEndWorkdayLengthInMinutes = workrules.AutoEndWorkday,
                    MinBreakBetweenWorkdaysInMinutes = workrules.MinBreakBetweenWorkdays,
                    MinWorkdayLengthForBreakInMinutes = workrules.MinWorkdayLengthForBreak,
                    MinBreakLengthInMinutes = workrules.MinBreakLength
                };
                _context.WorkSettings.Add(ws);
            }
            else
            {
                if(workrules.MinWorkdayLength != null)
                {
                    if(workrules.MinWorkdayLength < 0)
                    {
                        return BadRequest(new { message = "Work settings can't be negative." });
                    }
                    else
                    {
                        currWorkRules.MinWorkdayLengthInMinutes = workrules.MinWorkdayLength;
                    }
                }

                if (workrules.AutoEndWorkday != null)
                {
                    if (workrules.AutoEndWorkday < 0)
                    {
                        return BadRequest(new { message = "Work settings can't be negative." });
                    }
                    else
                    {
                        currWorkRules.AutoEndWorkdayLengthInMinutes = workrules.AutoEndWorkday;
                    }
                }

                if (workrules.MinBreakBetweenWorkdays != null)
                {
                    if (workrules.MinBreakBetweenWorkdays < 0)
                    {
                        return BadRequest(new { message = "Work settings can't be negative." });
                    }
                    else
                    {
                        currWorkRules.MinBreakBetweenWorkdaysInMinutes = workrules.MinBreakBetweenWorkdays;
                    }
                }

                if (workrules.MinWorkdayLengthForBreak != null)
                {
                    if (workrules.MinWorkdayLengthForBreak < 0)
                    {
                        return BadRequest(new { message = "Work settings can't be negative." });
                    }
                    else
                    {
                        currWorkRules.MinWorkdayLengthForBreakInMinutes = workrules.MinWorkdayLengthForBreak;
                    }
                }

                if (workrules.MinBreakLength != null)
                {
                    if (workrules.MinBreakLength < 0)
                    {
                        return BadRequest(new { message = "Work settings can't be negative." });
                    }
                    else
                    {
                        currWorkRules.MinBreakLengthInMinutes = workrules.MinBreakLength;
                    }
                }
            }
            await _context.SaveChangesAsync();
            return Ok(new {message = "Work rules changed successfully."});
        }
        
        public class NewWorkRules
        {
            public int? MinWorkdayLength { get; set; }
            public int? AutoEndWorkday { get; set; }
            public int? MinBreakBetweenWorkdays { get; set; }
            public int? MinWorkdayLengthForBreak { get; set; }
            public int? MinBreakLength { get; set; }
        }

        private bool WorkSettingsExists(int id)
        {
            return _context.WorkSettings.Any(e => e.ID == id);
        }
    }
}
