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
    public class DiplomController : APIBase
    {
        public DiplomController(AspirantDBContext ctx, IHttpContextAccessor accessor,
            ILogger<DiplomController> logger) : base(ctx, accessor, logger) { }

        [HttpGet]
        public async Task<ActionResult<object>> Get(int? id)
        {
            if (_user == null)
                return Unauthorized();

            if(id.HasValue)
            {
                if (_user.Role != Role.Admin)
                    return Forbid();

                var diplom = await _ctx.Diploms.AsNoTracking().FirstOrDefaultAsync(i => i.AspirantId == id.Value);
                if (diplom == null)
                    return NotFound();

                return new
                {
                    diplom.Id,
                    diplom.SpecialtyId,
                    diplom.AspirantId,
                    diplom.EndDate,
                    diplom.CountSatisfactoryMarks
                };
            }
            else
            {
                if (_aspirant == null)
                    return NotFound();
                var diplom = await _ctx.Diploms.AsNoTracking().FirstOrDefaultAsync(i => i.AspirantId == _aspirant.Id);
                if (diplom == null)
                    return NotFound();

                return new
                {
                    diplom.Id,
                    diplom.SpecialtyId,
                    diplom.AspirantId,
                    diplom.EndDate,
                    diplom.CountSatisfactoryMarks
                };
            }
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] DiplomAddForm form)
        {
            if (_user.Role != Role.Admin)
                return Forbid();

            var diplom = form.GetDiplom();
            await _ctx.Diploms.AddAsync(diplom);
            await _ctx.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Edit([FromQuery] int id, [FromBody] DiplomEditForm form)
        {
            if (_user.Role != Role.Admin)
                return Forbid();

            var diplom = await _ctx.Diploms.FirstOrDefaultAsync(i => i.Id == id);
            if (diplom == null)
                return NotFound();
            form.GetDiplom(diplom);
            await _ctx.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Remove([FromQuery] int id)
        {
            if (_user.Role != Role.Admin)
                return Forbid();

            var diplom = await _ctx.Diploms.FirstOrDefaultAsync(i => i.Id == id);
            if (diplom == null)
                return NotFound();
            _ctx.Diploms.Remove(diplom);
            await _ctx.SaveChangesAsync();
            return Ok();
        }
    }
}
