using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartMeterLibServices.Configurations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SmartMeterWebApp.SmartMeterApi.services;
using SmartMeterWebApp.SmartMeterApi.Data;

namespace SmartMeterWebApp.SmartMeterApi.Controllers
{
    [AllowAnonymous]
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IIdentityService _identityService;

        public AuthController(UserManager<IdentityUser> userManager, IIdentityService identityService,
            RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _identityService = identityService;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]loginViewModel vm)
        {

            var user = await _userManager.FindByEmailAsync(vm.username) ?? null;
            if (user == null)
                return BadRequest();
            var role = await _userManager.IsInRoleAsync(user, USERSCONFIG.Subscriber);
            if (!role)
                return BadRequest();


            var authResponse = await _identityService.LoginAsync(vm.username, vm.password);

            if (!authResponse.Success)
            {
                return BadRequest(new
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new
            {
                Token = authResponse.Token,
            });
        }

    }
}
