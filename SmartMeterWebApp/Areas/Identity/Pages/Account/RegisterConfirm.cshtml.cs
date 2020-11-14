
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace SmartMeterWebApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]

    public class RegisterConfirmModel : PageModel
    {

        private readonly UserManager<IdentityUser> _userManager;

        public RegisterConfirmModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;

        }

        public string Email { get; set; }

        public async Task<IActionResult> OnGetAsync(string email)
        {
            if (email == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound($"Unable to load user with email '{email}'.");
            }

            Email = email;
            return Page();
        }

    }
}
