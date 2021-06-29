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
    public class AbiturientController : APIBase
    {
        public AbiturientController(AspirantDBContext ctx, IHttpContextAccessor accessor,
            ILogger<AbiturientController> logger) : base(ctx, accessor, logger) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> List()
        {
            if (_user == null)
                return Unauthorized();
            if (_user.Role != Role.Admin)
                return Forbid();

            return await _ctx.Abiturients.AsNoTracking().Select(i => new
            {
                i.Id, i.PersonId, Specialties = i.SpecialtiesIndecies
            }).ToListAsync();
        }

        [HttpGet]
        public ActionResult<object> Self()
        {
            if (_user == null)
                return Unauthorized();

            if (_abiturient == null)
                return NotFound();

            return new
            {
                Specialties = _abiturient.SpecialtiesIndecies
            };
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] AbiturientForm form)
        {
            _logger.LogDebug($"[{DateTime.Now}]Attempting create abiturient");
            if (_user == null)
            {
                _logger.LogDebug($"Unauthorized");
                return Unauthorized();
            }

            if (_user.Role == Role.Admin)
            {
                _logger.LogDebug($"Admin attempt added person");
                return BadRequest();
            }

            if (_abiturient != null)
            {
                _logger.LogDebug($"Abiturient found. Bad request");
                return BadRequest();
            }

            _abiturient = new Abiturient
            {
                PersonId = _person.Id,
                Speciaties = string.Join(' ', form.Specialties),
                AdmissionDate = DateTime.Now
            };
            await _ctx.Abiturients.AddAsync(_abiturient);
            await _ctx.SaveChangesAsync();
            _logger.LogDebug($"Abiturient added");
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Edit([FromQuery] int? id, [FromBody] AbiturientForm form)
        {
            _logger.LogDebug($"[{DateTime.Now}]Attempting edit abiturient");

            if (_user == null)
            {
                _logger.LogDebug($"Unauthorized");
                return Unauthorized();
            }

            if (id.HasValue)
            {
                if (_user.Role != Role.Admin)
                {
                    _logger.LogDebug($"Access denied");
                    return Forbid();
                }
                var abiturient = await _ctx.Abiturients.FirstOrDefaultAsync(i => i.Id == id.Value);
                if (abiturient == null)
                {
                    _logger.LogDebug($"Abiturient not found");
                    return NotFound();
                }

                abiturient.Speciaties = string.Join(' ', form.Specialties);
                await _ctx.SaveChangesAsync();
                _logger.LogDebug($"Abiturient edited");
                return Ok();
            }
            else
            {
                if (_abiturient == null)
                {
                    _logger.LogDebug($"Abiturient not found");
                    return NotFound();
                }
                if (_user.Role != Role.Abiturient)
                    return Forbid();

                var abiturient = await _ctx.Abiturients.FirstOrDefaultAsync(i => i.Id == _abiturient.Id);
                abiturient.Speciaties = string.Join(' ', form.Specialties);
                await _ctx.SaveChangesAsync();
                _logger.LogDebug($"Abiturient edited");
                return Ok();
            }
        }
    }
}
