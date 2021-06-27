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
    public class SpecialtyController : APIBase
    {
        public SpecialtyController(AspirantDBContext ctx, IHttpContextAccessor accessor,
            ILogger<SpecialtyController> logger) : base(ctx, accessor, logger) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> List()
        {
            return await _ctx.Specialties.AsNoTracking().Select(i => new
            {
                i.Id,
                i.CathedraId,
                i.Title
            }).ToArrayAsync();
        }

        [HttpGet]
        public async Task<ActionResult<object>> Get(int id)
        {
            var cathedra = await _ctx.Specialties.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
            if (cathedra == null)
                return NotFound();
            return new
            {
                cathedra.Id,
                cathedra.CathedraId,
                cathedra.Title
            };
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] CathedraSpecialtyAddForm form)
        {
            if (_user == null)
                return Unauthorized();
            if (_user.Role != Role.Admin)
                return Forbid();

            var specialty = new Specialty { CathedraId = form.Id, Title = form.Title };
            await _ctx.Specialties.AddAsync(specialty);
            await _ctx.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Edit([FromQuery] int id, [FromBody] CathedraSpecialtyEditForm form)
        {
            if (_user == null)
                return Unauthorized();
            if (_user.Role != Role.Admin)
                return Forbid();

            var specialty = await _ctx.Specialties.FirstOrDefaultAsync(i => i.Id == id);
            if (specialty == null)
                return NotFound();
            if (form.Id.HasValue)
                specialty.CathedraId = form.Id.Value;
            if (!string.IsNullOrWhiteSpace(form.Title))
                specialty.Title = form.Title;
            await _ctx.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult<object>> Remove([FromQuery] int id)
        {
            if (_user == null)
                return Unauthorized();
            if (_user.Role != Role.Admin)
                return Forbid();

            var specialty = await _ctx.Specialties.FirstOrDefaultAsync(i => i.Id == id);
            if (specialty == null)
                return NotFound();
            _ctx.Specialties.Remove(specialty);
            await _ctx.SaveChangesAsync();
            return Ok();
        }
    }
}
