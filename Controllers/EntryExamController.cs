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
    public class EntryExamController : APIBase
    {
        public EntryExamController(AspirantDBContext ctx, IHttpContextAccessor accessor,
            ILogger<EntryExamController> logger) : base(ctx, accessor, logger) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> List()
        {
            if (_user == null)
                return Unauthorized();
            if (_person == null)
            {
                if (_user.Role != Role.Admin)
                    return Forbid();

                return await _ctx.EntryExams.AsNoTracking().Select(i => new
                {
                    i.Id,
                    i.SpecialtyId,
                    i.Subject,
                    i.Date
                }).ToListAsync();
            }
            else
            {
                if (_abiturient == null)
                    return NotFound();
                return await _ctx.EntryExams.AsNoTracking().Where(i => _abiturient.SpecialtiesIndecies.Contains(i.SpecialtyId.Value) &&
                i.Date.Year == _abiturient.AdmissionDate.Year).Select(i => new
                {
                    i.Id,
                    i.SpecialtyId,
                    i.Subject,
                    i.Date
                }).ToListAsync();
            }
        }

        [HttpGet]
        public async Task<ActionResult<object>> Get([FromQuery] int id)
        {
            if (_user == null)
                return Unauthorized();

            if (_abiturient == null && _user.Role != Role.Admin)
                return Forbid();

            var e = await _ctx.EntryExams.FirstOrDefaultAsync(i => i.Id == id && _abiturient.SpecialtiesIndecies.Contains(i.SpecialtyId.Value));
            if (e == null)
                return NotFound();
            return new
            {
                e.Id,
                e.SpecialtyId,
                e.Subject,
                e.Date
            };
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] EntryExamAddForm form)
        {
            if (_user == null)
                return Unauthorized();
            if (_user.Role != Role.Admin)
                return Forbid();

            var exam = form.GetExam();
            await _ctx.EntryExams.AddAsync(exam);
            await _ctx.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Edit([FromQuery] int id, [FromBody] EntryExamEditForm form)
        {
            if (_user == null)
                return Unauthorized();
            if (_user.Role != Role.Admin)
                return Forbid();

            var exam = await _ctx.EntryExams.FirstOrDefaultAsync(i => i.Id == id);
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

            var e = await _ctx.EntryExams.FirstOrDefaultAsync(i => i.Id == id);
            if (e == null)
                return NotFound();
            _ctx.EntryExams.Remove(e);
            await _ctx.SaveChangesAsync();
            return Ok();
        }
    }
}
