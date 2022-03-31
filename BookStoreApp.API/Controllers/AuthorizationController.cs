using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.User;
using BookStoreApp.API.Static;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly ILogger<AuthorizationController> logger;
        private readonly IMapper mapper;
        private readonly UserManager<ApiUser> userManager;
        private readonly IConfiguration configuration;

        public AuthorizationController(ILogger<AuthorizationController> logger, IMapper mapper, UserManager<ApiUser> userManager, IConfiguration configuration)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.userManager = userManager;
            this.configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserDTO userDTO)
        {
            var user = mapper.Map<ApiUser>(userDTO);
            user.UserName = userDTO.Email;
            var result = await userManager.CreateAsync(user, userDTO.Password);

            if (result.Succeeded == false)
            {
                logger.LogError("Unsuccessful registration:");
                foreach (var error in result.Errors)
                {
                    logger.LogError(error.Description);
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            var resultForRoleAdd = await userManager.AddToRoleAsync(user, userDTO.Role);

            return Accepted();
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthorizationResponse>> Login(LoginUserDTO userDTO)
        {
            logger.LogInformation($"Login Attempt for {userDTO}");
            try
            {
                var user = await userManager.FindByEmailAsync(userDTO.Email);
                if (user == null)
                {
                    return Unauthorized(userDTO);
                }
                var passwordValid = await userManager.CheckPasswordAsync(user, userDTO.Password);
                if(passwordValid == false)
                {
                    return Unauthorized(userDTO);
                }

                string tokenString = await GenerateToken(user);

                var response = new AuthorizationResponse
                {
                    Email = userDTO.Email,
                    Token = tokenString,
                    UserId = user.Id,
                };

                return Accepted(response);
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        private async Task<string> GenerateToken(ApiUser user)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var roles = await userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

            var userClaims  = await userManager.GetClaimsAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(CustomClaimTypes.Uid, user.Id)
            }
            .Union(roleClaims)
            .Union(userClaims)
            ;

            var token = new JwtSecurityToken(
                issuer: configuration["JwtSettings:Issuer"],
                audience: configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(Convert.ToInt32(configuration["JwtSettings:Duration"])),   
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
