using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using esencialAdmin.Data;
using esencialAdmin.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace esencialAdmin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    var econtext = services.GetRequiredService<esencialAdminContext>();

                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                    Task<bool> hasSeeded = DbInitializer.Initialize(context, econtext, userManager, roleManager);
                    hasSeeded.Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .Build();
    }
}
