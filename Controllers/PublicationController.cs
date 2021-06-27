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
    public class PublicationController : APIBase
    {
        public PublicationController(AspirantDBContext ctx, IHttpContextAccessor accessor,
            ILogger<PublicationController> logger) : base(ctx, accessor, logger) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> List([FromQuery] int? id)
        {
            if (_user == null)
                return Unauthorized();

            if (id.HasValue)
            {
                if (_user.Role != Role.Admin)
                    return Forbid();

                return await _ctx.Publications.AsNoTracking().Where(i => i.AspirantId == id.Value).Select(i => new
                {
                    i.Id,
                    i.AspirantId,
                    i.Subject,
                    i.SubjectEdit,
                    i.Journal,
                    i.JournalEdit,
                    i.Number,
                    i.NumberEdit,
                    i.Date,
                    i.DateEdit,
                    i.Page,
                    i.PageEdit,
                    i.Reason
                }).ToListAsync();
            }
            else
            {
                if (_aspirant == null)
                    return NotFound();

                return await _ctx.Publications.AsNoTracking().Where(i => i.AspirantId == _aspirant.Id).Select(i => new
                {
                    i.Id,
                    i.AspirantId,
                    i.Subject,
                    i.SubjectEdit,
                    i.Journal,
                    i.JournalEdit,
                    i.Number,
                    i.NumberEdit,
                    i.Date,
                    i.DateEdit,
                    i.Page,
                    i.PageEdit,
                    i.Reason
                }).ToListAsync();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] PublicationAddForm form)
        {
            if (_user == null)
                return Unauthorized();

            if (_user.Role == Role.Admin)
            {
                if (!await _ctx.Aspirants.AnyAsync(i => i.Id == form.AspirantId))
                    return NotFound();
                var publication = form.GetPublication();
                await _ctx.Publications.AddAsync(publication);
                await _ctx.SaveChangesAsync();
                return Ok();
            }
            else if (_user.Role == Role.Aspirant)
            {
                if (_aspirant == null)
                    return NotFound();
                var publication = form.GetPublication(true);
                publication.AspirantId = _aspirant.Id;
                await _ctx.Publications.AddAsync(publication);
                await _ctx.SaveChangesAsync();
                return Ok();
            }
            else
                return Forbid();
        }

        [HttpPost]
        public async Task<ActionResult> Edit([FromQuery] int id, [FromBody] PublicationEditForm form)
        {
            if (_user == null)
                return Unauthorized();

            if (_user.Role == Role.Admin)
            {
                if (!await _ctx.Aspirants.AnyAsync(i => i.Id == form.AspirantId))
                    return NotFound();
                var publication = await _ctx.Publications.FirstOrDefaultAsync(i => i.Id == id);
                if (publication == null)
                    return NotFound();
                form.GetPublication(publication);
                await _ctx.Publications.AddAsync(publication);
                await _ctx.SaveChangesAsync();
                return Ok();
            }
            else if (_user.Role == Role.Aspirant)
            {
                if (_aspirant == null)
                    return NotFound();
                var publication = await _ctx.Publications.FirstOrDefaultAsync(i => i.Id == id && i.AspirantId == _aspirant.Id);
                form.GetPublication(publication, true);
                await _ctx.SaveChangesAsync();
                return Ok();
            }
            else
                return Forbid();
        }

        [HttpDelete]
        public async Task<ActionResult> Remove([FromQuery] int id)
        {
            if (_user == null)
                return Unauthorized();

            if (_user.Role != Role.Admin)
                return Forbid();

            var publication = await _ctx.Publications.FirstOrDefaultAsync(i => i.Id == id);
            if (publication == null)
                return NotFound();
            _ctx.Publications.Remove(publication);
            await _ctx.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Approve([FromBody] ApproveObject obj)
        {
            if (_user == null)
                return Unauthorized();

            if (_user.Role != Role.Admin)
                return Forbid();

            var publication = await _ctx.Publications.FirstOrDefaultAsync(i => i.Id == obj.Id);
            if (publication == null)
                return NotFound();

            if (string.IsNullOrWhiteSpace(obj.Reason))
            {
                publication.SubjectEdit = null;
                publication.JournalEdit = null;
                publication.NumberEdit = null;
                publication.PageEdit = null;
                publication.DateEdit = null;
                publication.Reason = null;
                await _ctx.SaveChangesAsync();
                return Ok();
            }
            publication.Reason = obj.Reason;
            await _ctx.SaveChangesAsync();
            return Ok();
        }
    }
}
