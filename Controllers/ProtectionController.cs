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
    public class ProtectionController : APIBase
    {
        public ProtectionController(AspirantDBContext ctx, IHttpContextAccessor accessor,
            ILogger<ProtectionController> logger) : base(ctx, accessor, logger) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> List(int? id)
        {
            if (_user == null)
                return Unauthorized();

            if (id.HasValue)
            {
                if (_user.Role != Role.Admin)
                    return Forbid();

                return await _ctx.Protections.AsNoTracking().Where(i => i.AspirantId == id.Value).Select(i => new
                {
                    i.Id,
                    i.AspirantId,
                    i.DateTime,
                    i.City,
                    i.University,
                    i.Commission,
                    i.Result
                }).ToListAsync();
            }
            else
            {
                if (_aspirant == null)
                    return NotFound();

                return await _ctx.Protections.AsNoTracking().Where(i => i.AspirantId == _aspirant.Id).Select(i => new
                {
                    i.Id,
                    i.AspirantId,
                    i.DateTime,
                    i.City,
                    i.University,
                    i.Commission,
                    i.Result
                }).ToListAsync();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] ProtectionAddForm form)
        {
            if (_user.Role != Role.Admin)
                return Forbid();

            var protection = form.GetProtection();
            await _ctx.Protections.AddAsync(protection);
            await _ctx.SaveChangesAsync();

            var aspirant = await _ctx.Aspirants.AsNoTracking().FirstAsync(i => i.Id == protection.AspirantId);
            var person = await _ctx.People.AsNoTracking().FirstAsync(i => i.Id == aspirant.PersonId);
            var user = await _ctx.Users.FirstAsync(i => i.UserId == person.UserId);
            user.Role = Role.EndEnducation;

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Edit([FromQuery] int id, [FromBody] ProtectionEditForm form)
        {
            if (_user.Role != Role.Admin)
                return Forbid();

            var protection = await _ctx.Protections.FirstOrDefaultAsync(i => i.Id == id);
            if (protection == null)
                return NotFound();
            form.GetProtection(protection);
            await _ctx.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Remove([FromQuery] int id)
        {
            if (_user.Role != Role.Admin)
                return Forbid();

            var protection = await _ctx.Protections.FirstOrDefaultAsync(i => i.Id == id);
            if (protection == null)
                return NotFound();
            _ctx.Protections.Remove(protection);
            await _ctx.SaveChangesAsync();
            return Ok();
        }
    }
}
