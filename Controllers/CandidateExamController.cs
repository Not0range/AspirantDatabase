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
    public class CandidateExamController : APIBase
    {
        public CandidateExamController(AspirantDBContext ctx, IHttpContextAccessor accessor,
            ILogger<CandidateExamController> logger) : base(ctx, accessor, logger) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> List()
        {
            if (_user == null)
                return Unauthorized();
            if (_person == null)
            {
                if (_user.Role != Role.Admin)
                    return Forbid();

                return await _ctx.CandidateExams.AsNoTracking().Select(i => new
                {
                    i.Id,
                    i.SpecialtyId,
                    i.Subject,
                    i.DateTime
                }).ToListAsync();
            }
            else
            {
                if (_aspirant == null)
                    return NotFound();
                return await _ctx.CandidateExams.AsNoTracking().Where(i => i.SpecialtyId == _aspirant.SpecialtyId &&
                i.DateTime.Year >= _abiturient.AdmissionDate.Year + 4).Select(i => new
                {
                    i.Id,
                    i.SpecialtyId,
                    i.Subject,
                    i.DateTime
                }).ToListAsync();
            }
        }

        [HttpGet]
        public async Task<ActionResult<object>> Get([FromQuery] int id)
        {
            if (_user == null)
                return Unauthorized();

            if (_aspirant == null && _user.Role != Role.Admin)
                return Forbid();

            var e = await _ctx.CandidateExams.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id && _aspirant != null ? i.SpecialtyId == _aspirant.SpecialtyId : true);
            if (e == null)
                return NotFound();
            return new
            {
                e.Id,
                e.SpecialtyId,
                e.Subject,
                e.DateTime
            };
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] CandidateExamAddForm form)
        {
            if (_user == null)
                return Unauthorized();
            if (_user.Role != Role.Admin)
                return Forbid();

            var exam = form.GetExam();
            await _ctx.CandidateExams.AddAsync(exam);
            await _ctx.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Edit([FromQuery] int id, [FromBody] CandidateExamEditForm form)
        {
            if (_user == null)
                return Unauthorized();
            if (_user.Role != Role.Admin)
                return Forbid();

            var exam = await _ctx.CandidateExams.FirstOrDefaultAsync(i => i.Id == id);
            if (exam == null)
                return NotFound();
            form.GetExam(exam);
            await _ctx.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Remove([FromQuery] int id)
        {
            if (_user == null)
                return Unauthorized();
            if (_user.Role != Role.Admin)
                return Forbid();

            var e = await _ctx.CandidateExams.FirstOrDefaultAsync(i => i.Id == id);
            if (e == null)
                return NotFound();
            _ctx.CandidateExams.Remove(e);
            await _ctx.SaveChangesAsync();
            return Ok();
        }
    }
}
