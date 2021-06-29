using AspirantDatabase.Entities;
using AspirantDatabase.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AspirantDatabase.Controllers
{
    [ApiController]
    [EnableCors("Policy")]
    public class AbstractController : APIBase
    {
        public AbstractController(AspirantDBContext ctx, IHttpContextAccessor accessor,
            ILogger<AbstractController> logger) : base(ctx, accessor, logger) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> List([FromQuery] int? id)
        {
            if (_user == null)
                return Unauthorized();

            if (id.HasValue)
            {
                if (_user.Role != Role.Admin)
                    return Forbid();

                return await _ctx.Abstracts.AsNoTracking().Where(i => i.AspirantId == id.Value).Select(i => new
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

                return await _ctx.Abstracts.AsNoTracking().Where(i => i.AspirantId == _aspirant.Id).Select(i => new
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
        public async Task<ActionResult> Add([FromBody] AbstractAddForm form)
        {
            if (_user == null)
                return Unauthorized();

            if (_user.Role == Role.Admin)
            {
                if (!await _ctx.Aspirants.AnyAsync(i => i.Id == form.AspirantId))
                    return NotFound();
                var @abstract = form.GetAbstract();
                await _ctx.Abstracts.AddAsync(@abstract);
                await _ctx.SaveChangesAsync();
                return Ok();
            }
            else if (_user.Role == Role.Aspirant)
            {
                if (_aspirant == null)
                    return NotFound();
                var conference = form.GetAbstract(true);
                conference.AspirantId = _aspirant.Id;
                await _ctx.Abstracts.AddAsync(conference);
                await _ctx.SaveChangesAsync();
                return Ok();
            }
            else
                return Forbid();
        }

        [HttpPost]
        public async Task<ActionResult> Edit([FromQuery] int id, [FromBody] AbstractEditForm form)
        {
            if (_user == null)
                return Unauthorized();

            if (_user.Role == Role.Admin)
            {
                if (!await _ctx.Aspirants.AnyAsync(i => i.Id == form.AspirantId))
                    return NotFound();
                var @abstract = await _ctx.Abstracts.FirstOrDefaultAsync(i => i.Id == id);
                if (@abstract == null)
                    return NotFound();
                form.GetAbstract(@abstract);
                await _ctx.Abstracts.AddAsync(@abstract);
                await _ctx.SaveChangesAsync();
                return Ok();
            }
            else if (_user.Role == Role.Aspirant)
            {
                if (_aspirant == null)
                    return NotFound();
                var @abstract = await _ctx.Abstracts.FirstOrDefaultAsync(i => i.Id == id && i.AspirantId == _aspirant.Id);
                form.GetAbstract(@abstract, true);
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

            var @abstract = await _ctx.Abstracts.FirstOrDefaultAsync(i => i.Id == id);
            if (@abstract == null)
                return NotFound();
            _ctx.Abstracts.Remove(@abstract);
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

            var @abstract = await _ctx.Abstracts.FirstOrDefaultAsync(i => i.Id == obj.Id);
            if (@abstract == null)
                return NotFound();

            if(string.IsNullOrWhiteSpace(obj.Reason))
            {
                @abstract.SubjectEdit = null;
                @abstract.PlaceEdit = null;
                @abstract.DateTimeEdit = null;
                @abstract.Reason = null;
                await _ctx.SaveChangesAsync();
                return Ok();
            }
            @abstract.Reason = obj.Reason;
            await _ctx.SaveChangesAsync();
            return Ok();
        }
    }
}
