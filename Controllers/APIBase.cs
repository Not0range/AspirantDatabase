using AspirantDatabase.Entities;
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
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("Policy")]
    public class APIBase : ControllerBase
    {
        protected AspirantDBContext _ctx;

        protected IHttpContextAccessor _accessor;

        protected ILogger _logger;

        protected User _user;

        protected Person _person;

        protected Abiturient _abiturient;

        protected Aspirant _aspirant;

        public APIBase(AspirantDBContext ctx, IHttpContextAccessor accessor, ILogger logger)
        {
            _ctx = ctx;
            _accessor = accessor;
            _logger = logger;

            var id = accessor.HttpContext.User.Claims.FirstOrDefault(i => i.Type == "ID");
            if (accessor.HttpContext.User.Identity.IsAuthenticated && id != null)
            {
                _user = _ctx.Users.AsNoTracking().FirstOrDefault(i => i.UserId == int.Parse(id.Value));
                if (_user != null)
                {
                    _person = _ctx.People.AsNoTracking().FirstOrDefault(i => i.UserId == _user.UserId);
                    if (_person != null)
                    {
                        _abiturient = _ctx.Abiturients.AsNoTracking().FirstOrDefault(i => i.PersonId == _person.Id);
                        _aspirant = _ctx.Aspirants.AsNoTracking().FirstOrDefault(i => i.PersonId == _person.Id);
                    }
                }
            }
        }
    }
}
