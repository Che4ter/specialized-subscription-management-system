using essentialAdmin.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace essentialAdmin.Data
{
    public static class DbInitializer 
    {
        //This example just creates an Administrator role and one Admin users
        public async static Task<bool> Initialize(ApplicationDbContext context, essentialAdminContext essentialAdminContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //create database schema if none exists
            context.Database.EnsureCreated();
            essentialAdminContext.Database.EnsureCreated();

            if(!essentialAdminContext.Templates.Any())
            {
                var template1 = new Templates(){
                    Name = "Wein Etikette"
                };

                var template2 = new Templates()
                {
                    Name = "Olivenöl Etikette"
                };
                essentialAdminContext.Templates.Add(template1);
                essentialAdminContext.Templates.Add(template2);
            }

            //If there is already an User with Administrator role, abort
            string roleID = context.Roles.Where(r => r.Name == "Administrator").Select(r => r.Id).FirstOrDefault();
            if(roleID != null)
            {
                if (context.UserRoles.Any(r => r.RoleId == roleID))
                {
                    return true;
                }
            }

            //Create the Administartor Role
            await roleManager.CreateAsync(new IdentityRole("Administrator"));
            await roleManager.CreateAsync(new IdentityRole("Mitarbeiter"));

            //Create the default Admin account and apply the Administrator role
            string password = "1234";
            var user = new ApplicationUser();
            user.UserName = "admin@byom.de";
            user.Email = "admin@byom.de";
            await userManager.CreateAsync(user, password);
            await userManager.AddToRoleAsync(user, "Administrator");
            // userManager.AddToRoleAsync(await userManager.FindByNameAsync(user), "Administrator");
            return true;

        }
    }
}
