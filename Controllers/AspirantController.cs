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
    public class AspirantController : APIBase
    {
        public AspirantController(AspirantDBContext ctx, IHttpContextAccessor accessor,
            ILogger<AspirantController> logger) : base(ctx, accessor, logger) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> List()
        {
            if (_user == null)
                return Unauthorized();
            if (_user.Role != Role.Admin)
                return Forbid();

            return await _ctx.Aspirants.AsNoTracking().Select(i => new
            {
                i.Id,
                i.PersonId,
                i.ForeignLanguage,
                i.EnducationForm,
                i.EnducationDirection,
                i.SpecialtyId,
                i.Decree,
                i.DissertationTheme,
                i.TeacherId
            }).ToListAsync();
        }

        [HttpGet]
        public ActionResult<object> Self()
        {
            if (_user == null)
                return Unauthorized();

            if (_aspirant == null)
                return NotFound();

            return new
            {
                _aspirant.ForeignLanguage,
                _aspirant.EnducationForm,
                _aspirant.EnducationDirection,
                _aspirant.SpecialtyId,
                _aspirant.Decree,
                _aspirant.DissertationTheme,
                _aspirant.TeacherId
            };
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromQuery] int id, [FromBody] AspirantAddForm form)
        {
            _logger.LogDebug($"[{DateTime.Now}]Attempting create aspirant");
            if (_user == null)
            {
                _logger.LogDebug($"Unauthorized");
                return Unauthorized();
            }

            if (_user.Role != Role.Admin)
            {
                _logger.LogDebug($"Access denied");
                return Forbid();
            }

            var person = await _ctx.People.FirstOrDefaultAsync(i => i.Id == id);
            if(person == null)
            {
                _logger.LogDebug($"Person not fount");
                return NotFound();
            }

            _aspirant = form.GetAspirant();
            _aspirant.PersonId = id;
            await _ctx.Aspirants.AddAsync(_aspirant);
            var user = await _ctx.Users.FirstAsync(i => i.UserId == person.UserId);
            user.Role = Role.Aspirant;
            await _ctx.SaveChangesAsync();
            _logger.LogDebug($"Aspirant added");
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Edit([FromQuery] int id, [FromBody] AspirantEditForm form)
        {
            _logger.LogDebug($"[{DateTime.Now}]Attempting edit aspirant");
            if (_user == null)
            {
                _logger.LogDebug($"Unauthorized");
                return Unauthorized();
            }

            if (_user.Role != Role.Admin)
            {
                _logger.LogDebug($"Access denied");
                return Forbid();
            }

            var aspirant = await _ctx.Aspirants.FirstOrDefaultAsync(i => i.Id == id);
            if (aspirant == null)
            {
                _logger.LogDebug($"Aspirant not fount");
                return NotFound();
            }

            aspirant = form.GetAspirant(aspirant);
            await _ctx.Aspirants.AddAsync(aspirant);
            await _ctx.SaveChangesAsync();
            _logger.LogDebug($"Aspirant edited");
            return Ok();
        }
    }
}
