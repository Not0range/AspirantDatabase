using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspirantDatabase.Models;
using Microsoft.AspNetCore.Http;

namespace AspirantDatabase.Controllers
{
    [ApiController]
    [EnableCors("Policy")]
    public class AccountController : APIBase
    {
        public AccountController(AspirantDBContext ctx, IHttpContextAccessor accessor, 
            ILogger<AccountController> logger) : base(ctx, accessor, logger) { }

        [HttpPost]
        public async Task<ActionResult<object>> Login([FromBody] LoginForm info)
        {
            _logger.LogDebug($"[{DateTime.Now}]Attempting login");
            var user = await _ctx.Users.AsNoTracking().FirstOrDefaultAsync(u => (u.Username == info.Login || 
                u.Email == info.Login) && u.Password == info.Password);
            if (user == null)
            {
                _logger.LogDebug("Account not found");
                return NotFound();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("ID", user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.Role.GetString())
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity), new AuthenticationProperties
                {
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1)
                });

            _logger.LogDebug("Login success");
            return new { AccessLevel = (int)user.Role };
        }

        [HttpPost]
        public async Task<ActionResult<object>> Registration([FromBody] RegistrationForm info)
        {
            _logger.LogDebug($"[{DateTime.Now}]Attempting registration");
            var user = await _ctx.Users.FirstOrDefaultAsync(u => (u.Username == info.Username ||
                u.Email == info.Email) && u.Password == info.Password);

            if (user != null)
            {
                _logger.LogDebug("Account exist");
                return BadRequest();
            }
            user = new Entities.User
            {
                Username = info.Username,
                Email = info.Email,
                Password = info.Password,
                Role = Role.Abiturient
            };
            await _ctx.Users.AddAsync(user);
            await _ctx.SaveChangesAsync();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("ID", user.UserId.ToString()),
                new Claim(ClaimTypes.Role, Role.Abiturient.GetString())
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity), new AuthenticationProperties
                {
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1)
                });

            _logger.LogDebug("Registration success");
            return new { AccessLevel = (int)user.Role };
        }

        [HttpGet]
        public ActionResult<object> Check()
        {
            if (_user == null)
                return Unauthorized();
            return new { AccessLevel = (int)_user.Role };
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            _logger.LogDebug($"[{DateTime.Now}]Attempting logout");
            if (!User.Identity.IsAuthenticated)
            {
                _logger.LogDebug($"Unauthorized");
                return Unauthorized();
            }
            await HttpContext.SignOutAsync();
            _logger.LogDebug("Logout success");
            return Ok();
        }
    }
}
