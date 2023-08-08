using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        public readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.tokenRepository = tokenRepository;
            this.userManager = userManager;
        }

        // REGISTER
        // POST: /api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username,
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    // Add role to user
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registered!");
                    }
                }
            }

            return BadRequest("User could not be registered!");

        }

        // LOGIN
        // POST: /api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var identityUser = await userManager.FindByEmailAsync(loginRequestDto.Username);

            if (identityUser != null)
            {
                var result = await userManager.CheckPasswordAsync(identityUser, loginRequestDto.Password);

                if (result)
                {
                    var roles = await userManager.GetRolesAsync(identityUser);

                    if (roles != null && roles.Any())
                    {
                        // Create JWT
                        var jwt = tokenRepository.CreateJWTToken(identityUser, roles.ToList());
                        var loginResponse = new LoginResponseDto { Jwt = jwt };
                        return Ok(loginResponse);
                    }
                }
            }

            return Unauthorized();
        }
    }

    internal class LoginResponseDto
    {
        public string Jwt { get; set; }
    }
}
