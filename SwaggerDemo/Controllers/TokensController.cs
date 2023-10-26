using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SwaggerDemo.Data;
using SwaggerDemo.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private ApplicationDbContext _context;

        UserManager<ApplicationUser> userManager;
        SignInManager<ApplicationUser> signInManager;

        public TokensController(ApplicationDbContext context, SignInManager<ApplicationUser> _signInManager, UserManager<ApplicationUser> _userManager)
        {
            _context = context;
            signInManager = _signInManager;
            userManager = _userManager;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model)
        {

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            if (ModelState.IsValid)
            {
                var signInResult = await signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
                if (signInResult.Succeeded)
                {
                    var signingkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this-is-signing-key"));
                    var signingCredentials = new SigningCredentials(signingkey, SecurityAlgorithms.HmacSha256);
                    var jwt = new JwtSecurityToken(signingCredentials:signingCredentials,expires:DateTime.Now.AddMinutes(30));

                    return Ok(new JwtSecurityTokenHandler().WriteToken(jwt));
                }
            }

            return BadRequest(ModelState);

        }
    }
}
