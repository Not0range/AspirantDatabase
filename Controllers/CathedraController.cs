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
    public class CathedraController : APIBase
    {
        public CathedraController(AspirantDBContext ctx, IHttpContextAccessor accessor,
            ILogger<CathedraController> logger) : base(ctx, accessor, logger) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> List()
        {
            return await _ctx.Cathedras.AsNoTracking().Select(i => new
            {
                i.Id,
                i.FacultyId,
                i.Title
            }).ToArrayAsync();
        }

        [HttpGet]
        public async Task<ActionResult<object>> Get(int id)
        {
            var cathedra = await _ctx.Cathedras.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
            if (cathedra == null)
                return NotFound();
            return new
            {
                cathedra.Id,
                cathedra.FacultyId,
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

            var cathedra = new Cathedra { FacultyId = form.Id, Title = form.Title };
            await _ctx.Cathedras.AddAsync(cathedra);
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

            var cathedra = await _ctx.Cathedras.FirstOrDefaultAsync(i => i.Id == id);
            if (cathedra == null)
                return NotFound();
            if (form.Id.HasValue)
                cathedra.FacultyId = form.Id.Value;
            if(!string.IsNullOrWhiteSpace(form.Title))
                cathedra.Title = form.Title;
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

            var cathedra = await _ctx.Cathedras.FirstOrDefaultAsync(i => i.Id == id);
            if (cathedra == null)
                return NotFound();
            _ctx.Cathedras.Remove(cathedra);
            await _ctx.SaveChangesAsync();
            return Ok();
        }
    }
}
