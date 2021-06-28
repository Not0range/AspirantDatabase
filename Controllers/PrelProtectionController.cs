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
    public class PrelProtectionController : APIBase
    {
        public PrelProtectionController(AspirantDBContext ctx, IHttpContextAccessor accessor,
            ILogger<PrelProtectionController> logger) : base(ctx, accessor, logger) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> List(int? id)
        {
            if (_user == null)
                return Unauthorized();

            if (id.HasValue)
            {
                if (_user.Role != Role.Admin)
                    return Forbid();

                return await _ctx.PrelProtections.AsNoTracking().Where(i => i.AspirantId == id.Value).Select(i => new
                {
                    i.Id,
                    i.AspirantId,
                    i.DateTime,
                    i.Commission,
                    i.Allowmance
                }).ToListAsync();
            }
            else
            {
                if (_aspirant == null)
                    return NotFound();

                return await _ctx.PrelProtections.AsNoTracking().Where(i => i.AspirantId == _aspirant.Id).Select(i => new
                {
                    i.Id,
                    i.AspirantId,
                    i.DateTime,
                    i.Commission,
                    i.Allowmance
                }).ToListAsync();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] PrelProtectionAddForm form)
        {
            if (_user.Role != Role.Admin)
                return Forbid();

            var protection = form.GetProtection();
            await _ctx.PrelProtections.AddAsync(protection);
            await _ctx.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Edit([FromQuery] int id, [FromBody] PrelProtectionEditForm form)
        {
            if (_user.Role != Role.Admin)
                return Forbid();

            var protection = await _ctx.PrelProtections.FirstOrDefaultAsync(i => i.Id == id);
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

            var protection = await _ctx.PrelProtections.FirstOrDefaultAsync(i => i.Id == id);
            if (protection == null)
                return NotFound();
            _ctx.PrelProtections.Remove(protection);
            await _ctx.SaveChangesAsync();
            return Ok();
        }
    }
}
