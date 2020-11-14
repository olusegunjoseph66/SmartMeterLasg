using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace SmartMeterWebApp.Pages.Adminstrators.roles
{
    public class ManageStackerHoldersListRolesModel : PageModel
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;

        public ManageStackerHoldersListRolesModel(RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [BindProperty]
        public IEnumerable<IdentityRole> vm { get; set; }

        public async Task<IActionResult> OnGet()
        {
             vm = await roleManager.Roles.ToListAsync();
            return Page();
        }



        public async Task<IActionResult> OnPostDDeleteRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
   
                return NotFound("NotFound");
            }
            else
            {
                var result = await roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
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
