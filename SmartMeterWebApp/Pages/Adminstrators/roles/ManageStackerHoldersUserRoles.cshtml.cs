using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SmartMeterLibServices.ViewModel;

namespace SmartMeterWebApp.Pages.Adminstrators.roles
{
    public class ManageStackerHoldersUserRolesModel : PageModel
    {

        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public ManageStackerHoldersUserRolesModel(UserManager<IdentityUser> UserManager, RoleManager<IdentityRole> RoleManager)
        {
            userManager = UserManager;
            roleManager = RoleManager;
        }

   
        public class UserRolesViewModels
        {
            public UserRolesViewModels()
            {
              userRolesViews  = new List<UserRolesViewModel>();
            }
            public List<UserRolesViewModel> userRolesViews { get; set; } 
            public string userId { get; set; }
        }

        [BindProperty]
        public UserRolesViewModels vm { get; set; }

        public async Task<IActionResult> OnGet(string Id)
        {
        
            var user = await userManager.FindByIdAsync(Id);

            if (user == null)
            {
                return NotFound("NotFound");
            }

            var model = new List<UserRolesViewModel>();

            
            foreach (var role in await roleManager.Roles.ToListAsync())
            {
                var userRolesViewModel = new UserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.IsSelected = true;
                }
                else
                {
                    userRolesViewModel.IsSelected = false;
                }

                model.Add(userRolesViewModel);
            }

            var m = new UserRolesViewModels();
            m.userRolesViews = model;
            m.userId = Id;

            vm = m;
     
            return Page();
        }
        
        public async Task<IActionResult> OnPost()
        {
            var user = await userManager.FindByIdAsync(vm.userId);

            if (user == null)
            {
                return NotFound("NotFound");
            }
            //get and remove all the roles for the user..
            var roles = await userManager.GetRolesAsync(user);
            var result = await userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return Page();
            }

            result = await userManager.AddToRolesAsync(user,
                vm.userRolesViews.Where(x => x.IsSelected).Select(y => y.RoleName));

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return Page();
            }

            return RedirectToPage("/Adminstrators/users/ManagesStackholderEditeUsers", new { Id = vm.userId });
        }
    }
}

