using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Threading.Tasks;

namespace SmartMeterWebApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender emailSender;

        public ConfirmEmailModel(UserManager<IdentityUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            this.emailSender = emailSender;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);

            StatusMessage = result.Succeeded ? "Thank you for confirming your email; your password has been sent to your email for your login"
                                                                     + "You can Change your password in the setPassword section"
                                                                        :
               "Error confirming your email.";
            if (result.Succeeded)
            {
                await emailSender.SendEmailAsync(user.Email, "Your Password", $@"your login password is {user.UserName}; ");
                user.UserName = user.Email;
                await _userManager.UpdateAsync(user);
            }
            return Page();
        }
    }
}
