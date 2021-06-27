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
    public class EnducationController : APIBase
    {
        public EnducationController(AspirantDBContext ctx, IHttpContextAccessor accessor,
            ILogger<EnducationController> logger) : base(ctx, accessor, logger) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> List([FromQuery] int? id)
        {
            if (_user == null)
                return Unauthorized();
            if (id.HasValue)
            {
                if (_user.Role != Role.Admin)
                    return Forbid();

                return await _ctx.Enducations.AsNoTracking().Where(i => i.PersonId == id).Select(i => new
                {
                    i.Id,
                    i.PersonId,
                    i.Level,
                    i.Specialty,
                    i.EndDate,
                    i.Excellent,
                    i.CountSatisfactoryMarks
                }).ToListAsync();
            }
            else
            {
                if (_person == null)
                    return NotFound();
                return await _ctx.Enducations.AsNoTracking().Where(i => i.PersonId == _person.Id).Select(i => new
                {
                    i.Id,
                    i.PersonId,
                    i.Level,
                    i.Specialty,
                    i.EndDate,
                    i.Excellent,
                    i.CountSatisfactoryMarks
                }).ToListAsync();
            }
        }

        [HttpGet]
        public async Task<ActionResult<object>> Get([FromQuery] int id)
        {
            if (_user == null)
                return Unauthorized();

            if (_person == null && _user.Role != Role.Admin)
                return Forbid();

            var e = await _ctx.Enducations.FirstOrDefaultAsync(i => i.Id == id && _person != null ? i.PersonId == _person.Id : true);
            if (e == null)
                return NotFound();
            return new
            {
                e.Id,
                e.PersonId,
                e.Level,
                e.Specialty,
                e.EndDate,
                e.Excellent,
                e.CountSatisfactoryMarks
            };
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] EnducationAddForm form)
        {
            if (_user == null)
                return Unauthorized();
            if (_person == null)
                return NotFound();

            var enducation = form.GetEnducation();
            enducation.PersonId = _person.Id;
            await _ctx.Enducations.AddAsync(enducation);
            await _ctx.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Edit([FromQuery] int id, [FromBody] EnducationEditForm form)
        {
            if (_user == null)
                return Unauthorized();
            if (_person == null)
                return NotFound();

            var enducation = await _ctx.Enducations.FirstOrDefaultAsync(i => i.Id == id && i.PersonId == _person.Id);
            if (enducation == null)
                return NotFound();
            form.GetEnducation(enducation);
            await _ctx.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Remove([FromQuery] int id)
        {
            if (_user == null)
                return Unauthorized();
            if (_person == null && _user.Role != Role.Admin)
                return Forbid();

            var e = await _ctx.Enducations.FirstOrDefaultAsync(i => i.Id == id && _person != null ? i.PersonId == _person.Id : true);
            if (e == null)
                return NotFound();
            _ctx.Enducations.Remove(e);
            await _ctx.SaveChangesAsync();
            return Ok();
        }
    }
}
