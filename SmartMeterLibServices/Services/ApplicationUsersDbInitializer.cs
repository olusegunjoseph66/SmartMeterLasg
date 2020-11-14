

using Microsoft.AspNetCore.Identity;
using SmartMeterLibServices.Configurations;
using System.Threading.Tasks;
namespace SmartMeterLibServices.Services
{
    public static class ApplicationUsersDbInitializer
    {
        public async static Task<int?> SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync(USERSCONFIG.Admin).Result)
            {
                var role = new IdentityRole
                {
                    Name = USERSCONFIG.Admin
                };
                _ = await roleManager.CreateAsync(role);
            }
            if (!roleManager.RoleExistsAsync(USERSCONFIG.Subscriber).Result)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = USERSCONFIG.Subscriber
                };
                IdentityResult roleResult = await roleManager.CreateAsync(role);
            }
            if (!roleManager.RoleExistsAsync(USERSCONFIG.StackHolder).Result)
            {
                var role = new IdentityRole();
                role.Name = USERSCONFIG.StackHolder;
                IdentityResult roleResult = await roleManager.CreateAsync(role);
            }
        
            return 1;
        }
        public async static Task SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByEmailAsync(USERSCONFIG.Admin_Email).Result == null)
            {
                var user = new IdentityUser();
                user.Email = USERSCONFIG.Admin_Email;
                user.EmailConfirmed = true;
                user.LockoutEnabled = true;
                user.UserName = USERSCONFIG.Admin_Email;
                IdentityResult result = await userManager.CreateAsync(user, USERSCONFIG.Admin_Password);
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, USERSCONFIG.Admin).Wait();
                }
            }
            if (userManager.FindByEmailAsync(USERSCONFIG.Subscriber_Email).Result == null)
            {
                var user = new IdentityUser();
                user.Email = USERSCONFIG.Subscriber_Email;
                user.EmailConfirmed = true;
                user.LockoutEnabled = true;
                user.UserName = USERSCONFIG.Subscriber_Email;
                IdentityResult result = await userManager.CreateAsync(user, USERSCONFIG.Subscriber_Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, USERSCONFIG.Subscriber);
                }
            }
            if (userManager.FindByEmailAsync(USERSCONFIG.Stackholders_Email).Result == null)
            {
                var user = new IdentityUser();
                user.Email = USERSCONFIG.Stackholders_Email;
                user.EmailConfirmed = true;
                user.LockoutEnabled = true;
                user.UserName = USERSCONFIG.Stackholders_Email;
                IdentityResult result = await userManager.CreateAsync(user, USERSCONFIG.Stackholders_Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, USERSCONFIG.StackHolder);
                }
            }

        }
        public async static Task SeedData(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
            context.Database.EnsureCreated();
            var res = await SeedRoles(roleManager);
            if (res != null)
                await SeedUsers(userManager);
        }

    }
}