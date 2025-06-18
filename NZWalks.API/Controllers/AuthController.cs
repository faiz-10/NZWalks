using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;


        public AuthController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        // POST: api/auth/register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.UserName,
                Email = registerRequestDto.UserName // Assuming UserName is an email address
            };

            var identityResults = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResults.Succeeded)
            {
                if(registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    // If roles are provided, add the user to those roles
                    identityResults = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);

                    if (identityResults.Succeeded)
                    {
                        return Ok("User was registered! Please login.");
                    }
                }
                
            }

            return BadRequest("Something went wrong");
        }

        // POST: api/auth/login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var identityUser = await userManager.FindByEmailAsync(loginRequestDto.UserName);

            if(identityUser != null)
            {
                var results = await userManager.CheckPasswordAsync(identityUser, loginRequestDto.Password);

                if (results)
                {
                    // create a JWT token here (not implemented in this example)

                    return Ok();
                }
            }

            return BadRequest("Invalid credentials");
        }
    }
}
