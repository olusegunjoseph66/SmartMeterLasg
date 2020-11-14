using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using SmartMeterLibServices.Maps;
using SmartMeterLibServices.Reprository;
using SmartMeterLibServices.ViewModel;
using System;
using System.Text;
using System.Threading.Tasks;

namespace SmartMeterWebApp.Pages.Adminstrators
{
    [Authorize(Roles = "Admin")]
    public class CraeteStakeholderAccountsModel : PageModel
    {
        private readonly IStackHolderRepository _StackHolderRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;

        public CraeteStakeholderAccountsModel(IStackHolderRepository StackHolderRepository, UserManager<IdentityUser> UserManager,
            RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> SignInManager, IEmailSender emailSender,
            IConfiguration configuration)
        {
            _StackHolderRepository = StackHolderRepository;
            _userManager = UserManager;
            _roleManager = roleManager;
            _signInManager = SignInManager;
            _emailSender = emailSender;
            _configuration = configuration;
        }

        [BindProperty]
        public StackholderCreateViewModel stackholderInfo { get; set; }


        public IActionResult OnGet()
        {
            stackholderInfo = new StackholderCreateViewModel();
            return Page();
        }


        public async Task<IActionResult> OnPost()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var pass = Guid.NewGuid().ToString().Substring(0, 7);

            var user = new IdentityUser { Email = stackholderInfo.Email, UserName = pass, PhoneNumber = stackholderInfo.Phone };

            var result = await _userManager.CreateAsync(user, pass);

            if (!await _roleManager.RoleExistsAsync("StackHolder"))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "StackHolder" });
            }
            await _userManager.AddToRoleAsync(user, "StackHolder");

            if (result.Succeeded)
            {

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                var callbackUrl = Url.Page(
                                        "/Account/ConfirmEmail",
                                        pageHandler: null,
                                        values: new { area = "Identity", userId = user.Id, code = code },
                                        protocol: Request.Scheme);


                await _emailSender.SendEmailAsync(stackholderInfo.Email, "Confirm your email", $"Please confirm your account by <a href='{callbackUrl}'>clicking here</a>.");

                stackholderInfo.UserId = user.Id;
                await _StackHolderRepository.Add(stackholderInfo.Map());

            }
            return RedirectToPage("/Account/RegisterConfirm", new { area = "Identity", email = stackholderInfo.Email });
        }
    }
}