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
    public class ConferenceController : APIBase
    {
        public ConferenceController(AspirantDBContext ctx, IHttpContextAccessor accessor,
            ILogger<ConferenceController> logger) : base(ctx, accessor, logger) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> List([FromQuery] int? id)
        {
            if (_user == null)
                return Unauthorized();

            if(id.HasValue)
            {
                if (_user.Role != Role.Admin)
                    return Forbid();

                return await _ctx.Conferences.AsNoTracking().Where(i => i.AspirantId == id.Value).Select(i => new
                {
                    i.Id,
                    i.AspirantId,
                    i.Subject,
                    i.SubjectEdit,
                    i.DateTime,
                    i.DateTimeEdit,
                    i.Place,
                    i.PlaceEdit,
                    i.Reason
                }).ToListAsync();
            }
            else
            {
                if (_aspirant == null)
                    return NotFound();

                return await _ctx.Conferences.AsNoTracking().Where(i => i.AspirantId == _aspirant.Id).Select(i => new
                {
                    i.Id,
                    i.AspirantId,
                    i.Subject,
                    i.SubjectEdit,
                    i.DateTime,
                    i.DateTimeEdit,
                    i.Place,
                    i.PlaceEdit,
                    i.Reason
                }).ToListAsync();
            }
        }
        
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] ConferenceAddForm form)
        {
            if (_user == null)
                return Unauthorized();

            if (_user.Role == Role.Admin)
            {
                if (!await _ctx.Aspirants.AnyAsync(i => i.Id == form.AspirantId))
                    return NotFound();
                var conference = form.GetConference();
                await _ctx.Conferences.AddAsync(conference);
                await _ctx.SaveChangesAsync();
                return Ok();
            }
            else if (_user.Role == Role.Aspirant)
            {
                if (_aspirant == null)
                    return NotFound();
                var conference = form.GetConference(true);
                conference.AspirantId = _aspirant.Id;
                await _ctx.Conferences.AddAsync(conference);
                await _ctx.SaveChangesAsync();
                return Ok();
            }
            else
                return Forbid();
        }

        [HttpPost]
        public async Task<ActionResult> Edit([FromQuery] int id, [FromBody] ConferenceEditForm form)
        {
            if (_user == null)
                return Unauthorized();

            if (_user.Role == Role.Admin)
            {
                if (!await _ctx.Aspirants.AnyAsync(i => i.Id == form.AspirantId))
                    return NotFound();
                var conference = await _ctx.Conferences.FirstOrDefaultAsync(i => i.Id == id);
                if (conference == null)
                    return NotFound();
                form.GetConference(conference);
                await _ctx.Conferences.AddAsync(conference);
                await _ctx.SaveChangesAsync();
                return Ok();
            }
            else if (_user.Role == Role.Aspirant)
            {
                if (_aspirant == null)
                    return NotFound();
                var conference = await _ctx.Conferences.FirstOrDefaultAsync(i => i.Id == id && i.AspirantId == _aspirant.Id);
                form.GetConference(conference, true);
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

            var conference = await _ctx.Conferences.FirstOrDefaultAsync(i => i.Id == id);
            if (conference == null)
                return NotFound();
            _ctx.Conferences.Remove(conference);
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

            var conference = await _ctx.Conferences.FirstOrDefaultAsync(i => i.Id == obj.Id);
            if (conference == null)
                return NotFound();

            if (string.IsNullOrWhiteSpace(obj.Reason))
            {
                conference.SubjectEdit = null;
                conference.PlaceEdit = null;
                conference.DateTimeEdit = null;
                conference.Reason = null;
                await _ctx.SaveChangesAsync();
                return Ok();
            }
            conference.Reason = obj.Reason;
            await _ctx.SaveChangesAsync();
            return Ok();
        }
    }
}
