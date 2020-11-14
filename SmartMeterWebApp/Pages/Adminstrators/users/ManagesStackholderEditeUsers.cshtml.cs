using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Razor.Language;
using SmartMeterLibServices.ViewModel;

namespace SmartMeterWebApp.Pages.Adminstrators.users
{
    public class ManagesStackholderEditeUsersModel : PageModel
    {

        private readonly UserManager<IdentityUser> _UserManager;
        private readonly RoleManager<IdentityRole> _RoleManager;
        public ManagesStackholderEditeUsersModel(UserManager<IdentityUser> UserManager, RoleManager<IdentityRole> RoleManager)
        {
            _UserManager = UserManager;
            _RoleManager = RoleManager;
        }
        [BindProperty]
        public StackHolderEditUserVieweModel vm { get; set; }


        public async Task<IActionResult> OnGet(string Id)
        {
            var user = await _UserManager.FindByIdAsync(Id);
            if (user == null)
            {
                return NotFound();
            }

            var userclaims = await _UserManager.GetClaimsAsync(user);
            var userroles = await _UserManager.GetRolesAsync(user);

            var model = new StackHolderEditUserVieweModel
            {
                Id = user.Id,
                Email=user.Email,
                UserName = user.UserName,
                Roles =  userroles,           
            };
            vm = model;
            return Page();

        }

        public async Task<IActionResult> OnPost()
        {
            var user = await _UserManager.FindByIdAsync(vm.Id);

            if (user == null)
            {
            
                return NotFound("NotFound");
            }
            else
            {
                user.Email = vm.Email;
                user.UserName = vm.UserName;

                var result = await _UserManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToPage("/adminstrators/users/managestackerholderslistusers");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return Page();
            }
        }


    }
}
