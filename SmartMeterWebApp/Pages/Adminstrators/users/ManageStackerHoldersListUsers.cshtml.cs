using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartMeterLibServices.Configurations;

namespace SmartMeterWebApp.Pages.Adminstrators
{
    public class ManageStackerHoldersListUsersModel : PageModel
    {
        private readonly UserManager<IdentityUser> _UserManager;
        private readonly RoleManager<IdentityRole> _RoleManager;
        public ManageStackerHoldersListUsersModel( UserManager<IdentityUser> UserManager, RoleManager<IdentityRole> RoleManager)
        {
            _UserManager = UserManager;
            _RoleManager = RoleManager;
        }

        [BindProperty]
        public IEnumerable<IdentityUser> Users { get; set; }
       

        public async Task OnGet()
        {
            if (await _RoleManager.RoleExistsAsync(USERSCONFIG.StackHolder))
                Users = await _UserManager.GetUsersInRoleAsync(USERSCONFIG.StackHolder); 
        }


        public async Task<IActionResult> OnPostDeleteUser(string id)
        {
            var res = await _UserManager.DeleteAsync(await _UserManager.FindByIdAsync(id));
            if(res.Succeeded)
            return Page();

            return NotFound($"no user with id = {id} found");
        }

       
        
    }
}
