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
    public class FacultyController : APIBase
    {
        public FacultyController(AspirantDBContext ctx, IHttpContextAccessor accessor,
            ILogger<FacultyController> logger) : base(ctx, accessor, logger) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> List()
        {
            return await _ctx.Faculties.AsNoTracking().Select(i => new
            {
                i.Id,
                i.Title
            }).ToArrayAsync();
        }

        [HttpGet]
        public async Task<ActionResult<object>> Get(int id)
        {
            var faculty = await _ctx.Faculties.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
            if (faculty == null)
                return NotFound();
            return new
            {
                faculty.Id,
                faculty.Title
            };
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] FacultyForm form)
        {
            if (_user == null)
                return Unauthorized();
            if (_user.Role != Role.Admin)
                return Forbid();

            var faculty = new Faculty { Title = form.Title };
            await _ctx.Faculties.AddAsync(faculty);
            await _ctx.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Edit([FromQuery] int id, [FromBody] FacultyForm form)
        {
            if (_user == null)
                return Unauthorized();
            if (_user.Role != Role.Admin)
                return Forbid();

            var faculty = await _ctx.Faculties.FirstOrDefaultAsync(i => i.Id == id);
            if (faculty == null)
                return NotFound();
            faculty.Title = form.Title;
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

            var faculty = await _ctx.Faculties.FirstOrDefaultAsync(i => i.Id == id);
            if (faculty == null)
                return NotFound();
            _ctx.Faculties.Remove(faculty);
            await _ctx.SaveChangesAsync();
            return Ok();
        }
    }
}
