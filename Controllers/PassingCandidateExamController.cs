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
    public class PassingCandidateExamController : APIBase
    {
        public PassingCandidateExamController(AspirantDBContext ctx, IHttpContextAccessor accessor,
            ILogger<PassingCandidateExamController> logger) : base(ctx, accessor, logger) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> List(int? id)
        {
            if (_user == null)
                return Unauthorized();

            if (id.HasValue)
            {
                if (_user.Role != Role.Admin)
                    return Forbid();

                return await _ctx.PassingCandidateExams.AsNoTracking().Where(i => i.AspirantId == id.Value).Select(i => new
                {
                    i.Id,
                    i.ExamId,
                    i.AspirantId,
                    i.Result
                }).ToListAsync();
            }
            else
            {
                if (_aspirant == null)
                    return NotFound();

                return await _ctx.PassingCandidateExams.AsNoTracking().Where(i => i.AspirantId == _aspirant.Id).Select(i => new
                {
                    i.Id,
                    i.ExamId,
                    i.AspirantId,
                    i.Result
                }).ToListAsync();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] PassingExamAddForm form)
        {
            if (_user.Role != Role.Admin)
                return Forbid();

            var passing = form.GetPassingCandidate();
            await _ctx.PassingCandidateExams.AddAsync(passing);
            await _ctx.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Edit([FromQuery] int id, [FromBody] PassingExamEditForm form)
        {
            if (_user.Role != Role.Admin)
                return Forbid();

            var passing = await _ctx.PassingCandidateExams.FirstOrDefaultAsync(i => i.Id == id);
            if (passing == null)
                return NotFound();
            form.GetPassingCandidate(passing);
            await _ctx.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Remove([FromQuery] int id)
        {
            if (_user.Role != Role.Admin)
                return Forbid();

            var passing = await _ctx.PassingCandidateExams.FirstOrDefaultAsync(i => i.Id == id);
            if (passing == null)
                return NotFound();
            _ctx.PassingCandidateExams.Remove(passing);
            await _ctx.SaveChangesAsync();
            return Ok();
        }
    }
}
