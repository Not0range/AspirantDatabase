using AspirantDatabase.Entities;
using AspirantDatabase.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspirantDatabase.Controllers
{
    [ApiController]
    [EnableCors("Policy")]
    public class PassingEntryExamController : APIBase
    {
        public PassingEntryExamController(AspirantDBContext ctx, IHttpContextAccessor accessor,
            ILogger<PassingEntryExamController> logger) : base(ctx, accessor, logger) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> List(int? id)
        {
            if (_user == null)
                return Unauthorized();

            if (id.HasValue)
            {
                if (_user.Role != Role.Admin)
                    return Forbid();

                return await _ctx.PassingEntryExams.AsNoTracking().Where(i => i.AbiturientId == id.Value).Select(i => new
                {
                    i.Id,
                    i.ExamId,
                    i.AbiturientId,
                    i.Result
                }).ToListAsync();
            }
            else
            {
                if (_abiturient == null)
                    return NotFound();

                return await _ctx.PassingEntryExams.AsNoTracking().Where(i => i.AbiturientId == _abiturient.Id).Select(i => new
                {
                    i.Id,
                    i.ExamId,
                    i.AbiturientId,
                    i.Result
                }).ToListAsync();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] PassingExamAddForm form)
        {
            if (_user.Role != Role.Admin)
                return Forbid();

            var passing = form.GetPassingEntry();
            await _ctx.PassingEntryExams.AddAsync(passing);
            await _ctx.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Edit([FromQuery] int id, [FromBody] PassingExamEditForm form)
        {
            if (_user.Role != Role.Admin)
                return Forbid();

            var passing = await _ctx.PassingEntryExams.FirstOrDefaultAsync(i => i.Id == id);
            if (passing == null)
                return NotFound();
            form.GetPassingEntry(passing);
            await _ctx.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Remove([FromQuery] int id)
        {
            if (_user.Role != Role.Admin)
                return Forbid();

            var passing = await _ctx.PassingEntryExams.FirstOrDefaultAsync(i => i.Id == id);
            if (passing == null)
                return NotFound();
            _ctx.PassingEntryExams.Remove(passing);
            await _ctx.SaveChangesAsync();
            return Ok();
        }
    }
}
