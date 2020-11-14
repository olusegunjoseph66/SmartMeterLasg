using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using SmartMeterLibServices;
using SmartMeterLibServices.Model;
using SmartMeterLibServices.Reprository;

namespace SmartMeterWebApp.Pages.Stackholders
{
    public class RegisterSubscriberModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int id { get; set; }
        private readonly ISubscriberRepository _SubscriberData; private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        public List<SelectListItem> Genders { get; set; }
            = new List<SelectListItem>
           {
                new SelectListItem("Male", "M"),
                new SelectListItem("Female", "F")
           };
        [DataType(DataType.Date)]
        [BindProperty(SupportsGet = true)]
        public DateTime DOfB { get; set; } = DateTime.Now;

        public RegisterSubscriberModel(ISubscriberRepository subscriberData, UserManager<IdentityUser> UserManager,
            RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> SignInManager, IEmailSender emailSender,
            IConfiguration configuration)
        {
            _SubscriberData = subscriberData;
            _userManager = UserManager;
            _roleManager = roleManager;
            _signInManager = SignInManager;
            _emailSender = emailSender;
            _configuration = configuration;
        }
        [BindProperty]
        public SubscriberInfoModel subscribInfo { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id.HasValue)
            {
                subscribInfo = _SubscriberData.GetSubscriber(id.Value);

            }
            else
            {
                subscribInfo = new SubscriberInfoModel();
            }

            if (subscribInfo == null)   
            {
                return RedirectToPage("/Error");
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (subscribInfo.Subscriber_ID > 0)
            {

            }
            else
            {


                if (!ModelState.IsValid)
                {
                    return Page();
                }

                var pass = Guid.NewGuid().ToString().Substring(0, 7);

                var user = new IdentityUser
                {
                    Email = subscribInfo.Email,
                    UserName = subscribInfo.Email,
                    PhoneNumber = pass,
                    EmailConfirmed = true,
                    LockoutEnabled = true
                };

                var result = await _userManager.CreateAsync(user, pass);

                if (!await _roleManager.RoleExistsAsync("Subcriber"))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = "Subcriber" });
                }
                await _userManager.AddToRoleAsync(user, "Subcriber");

                if (result.Succeeded)
                {
                    await _emailSender.SendEmailAsync(user.Email, "SmartMeter Login", $@"Username : {user.Email} 
                     Password :  {pass}");
                }
                //send the login details to email
                subscribInfo.STAKEHOLDER_ID = User.Identity.Name;
                subscribInfo.UserId = (await _userManager.FindByEmailAsync(subscribInfo.Email)).Id;
                _SubscriberData.Add(subscribInfo);
            }




            return RedirectToPage("/Stackholders/Subscribers");
        }
    }
}