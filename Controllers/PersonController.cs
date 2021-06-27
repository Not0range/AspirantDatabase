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
    public class PersonController : APIBase
    {
        public PersonController(AspirantDBContext ctx, IHttpContextAccessor accessor,
            ILogger<PersonController> logger) : base(ctx, accessor, logger) { }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> List()
        {
            if (_user == null)
                return Unauthorized();
            if (_user.Role != Role.Admin)
                return Forbid();
            return await _ctx.People.AsNoTracking().Select(i => new 
            {
                i.Id,
                i.Lastname, 
                i.Firstname,
                i.Patronymic, 
                i.Birthdate, 
                i.Citizenship, 
                i.Passport, 
                i.Workbook,
                i.Workplaces,
                i.Contacts
            }).ToListAsync();
        }

        [HttpGet]
        public ActionResult<object> Self()
        {
            if (_user == null)
                return Unauthorized();
            if (_person == null)
                NotFound();
            return new
            {
                _person.Lastname,
                _person.Firstname,
                _person.Patronymic,
                _person.Birthdate,
                _person.Citizenship,
                _person.Passport,
                _person.Workbook,
                _person.Workplaces,
                _person.Contacts
            };
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] PersonAddForm form)
        {
            _logger.LogDebug($"[{DateTime.Now}]Attempting create person");
            if (_user == null)
            {
                _logger.LogDebug($"Unauthorized");
                return Unauthorized();
            }

            if(_user.Role == Role.Admin)
            {
                _logger.LogDebug($"Admin attempt added person");
                return BadRequest();
            }

            if ( _person != null)
            {
                _logger.LogDebug($"Person found. Bad request");
                return BadRequest();
            }

            _person = form.GetPerson();
            _person.UserId = _user.UserId;
            await _ctx.People.AddAsync(_person);
            await _ctx.SaveChangesAsync();
            _logger.LogDebug($"Person added ({_person.Lastname} {_person.Firstname} {_person.Patronymic})");
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Edit([FromQuery]int? id, [FromBody] PersonEditForm form)
        {
            _logger.LogDebug($"[{DateTime.Now}]Attempting edit person");

            if (_user == null)
            {
                _logger.LogDebug($"Unauthorized");
                return Unauthorized();
            }

            if (id.HasValue)
            {
                if(_user.Role != Role.Admin)
                {
                    _logger.LogDebug($"Access denied");
                    return Forbid();
                }
                var person = await _ctx.People.FirstOrDefaultAsync(i => i.Id == id.Value);
                if(person == null)
                {
                    _logger.LogDebug($"Person not found");
                    return NotFound();
                }

                form.GetPerson(_person);
                await _ctx.SaveChangesAsync();
                _logger.LogDebug($"Person edited ({_person.Lastname} {_person.Firstname} {_person.Patronymic})");
                return Ok();
            }
            else
            {
                if (_person == null)
                {
                    _logger.LogDebug($"Person not found");
                    return NotFound();
                }
                if (_user.Role == Role.EndEnducation)
                    return Forbid();

                var person = await _ctx.People.FirstAsync(i => i.Id == _person.Id);
                form.GetPerson(person);
                await _ctx.SaveChangesAsync();
                _logger.LogDebug($"Person edited ({person.Lastname} {person.Firstname} {person.Patronymic})");
                return Ok();
            }
        }
    }
}
