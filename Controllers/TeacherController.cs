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
    public class TeacherController : APIBase
    {
        public TeacherController(AspirantDBContext ctx, IHttpContextAccessor accessor,
            ILogger<TeacherController> logger) : base(ctx, accessor, logger) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> List()
        {
            return await _ctx.Teachers.AsNoTracking().Select(i => new
            {
                i.Id,
                i.Lastname,
                i.Firstname,
                i.Patronymic,
                i.BirthDate,
                i.CathedraId,
                i.Rank,
                i.Position
            }).ToArrayAsync();
        }

        [HttpGet]
        public async Task<ActionResult<object>> Get(int id)
        {
            var teacher = await _ctx.Teachers.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
            if (teacher == null)
                return NotFound();
            return new
            {
                teacher.Id,
                teacher.Lastname,
                teacher.Firstname,
                teacher.Patronymic,
                teacher.BirthDate,
                teacher.CathedraId,
                teacher.Rank,
                teacher.Position
            };
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] TeacherAddForm form)
        {
            if (_user == null)
                return Unauthorized();
            if (_user.Role != Role.Admin)
                return Forbid();

            var teacher = form.GetTeacher();
            await _ctx.Teachers.AddAsync(teacher);
            await _ctx.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Edit([FromQuery] int id, [FromBody] TeacherEditForm form)
        {
            if (_user == null)
                return Unauthorized();
            if (_user.Role != Role.Admin)
                return Forbid();

            var teacher = await _ctx.Teachers.FirstOrDefaultAsync(i => i.Id == id);
            if (teacher == null)
                return NotFound();
            form.GetTeacher(teacher);
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

            var teacher = await _ctx.Teachers.FirstOrDefaultAsync(i => i.Id == id);
            if (teacher == null)
                return NotFound();
            _ctx.Teachers.Remove(teacher);
            await _ctx.SaveChangesAsync();
            return Ok();
        }
    }
}
