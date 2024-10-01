using LBWalksAPI.Models.DTO;
using LBWalksAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LBWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }






        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.UserName
            };

             var identityResult = await userManager.CreateAsync(identityUser, registerDto.Password);
            if (identityResult.Succeeded)
            {
                if (registerDto.Roles != null && registerDto.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerDto.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registerd successfully! Please login");
                    }

                }
            }
            return BadRequest("Something went wrong");
        }














        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
           var user =  await userManager.FindByEmailAsync(loginDto.UserName);

            if (user != null)
            {
               var checkPassResult = await userManager.CheckPasswordAsync(user, loginDto.Password);
                if (checkPassResult)
                {
                    // Get the Roles for this user
                   var roles = await userManager.GetRolesAsync(user);
                    if (roles != null)
                    {

                        // Create Token
                       var jwtToken =  tokenRepository.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        };

                        return Ok(response);
                    }                     
                }
            }

            return BadRequest("Wrong Username or Password");
        }
    }
}
