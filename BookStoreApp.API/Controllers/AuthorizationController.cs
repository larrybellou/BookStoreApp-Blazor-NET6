using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly ILogger<AuthorizationController> logger;
        private readonly IMapper mapper;
        private readonly UserManager<ApiUser> userManager;

        public AuthorizationController(ILogger<AuthorizationController> logger, IMapper mapper, UserManager<ApiUser> userManager)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.userManager = userManager;
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
        public async Task<IActionResult> Login(LoginUserDTO userDTO)
        {
            logger.LogInformation($"Login Attempt for {userDTO}");
            try
            {
                var user = await userManager.FindByEmailAsync(userDTO.Email);
                if (user == null)
                {
                    return NotFound();
                }
                var passwordValid = await userManager.CheckPasswordAsync(user, userDTO.Password);
                if(passwordValid == false)
                {
                    return NotFound();
                }

                return Accepted();
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
    }
}
