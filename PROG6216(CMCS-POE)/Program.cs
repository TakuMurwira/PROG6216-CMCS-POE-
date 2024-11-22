using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PROG6216_CMCS_POE_.DataAccesLayer;

namespace PROG6216_CMCS_POE_
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services
            builder.Services.AddDbContext<ClaimsDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
                options.SignIn.RequireConfirmedAccount = false) // Set to 'true' in production
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ClaimsDbContext>();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Seed roles and admin user
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    await SeedRolesAndAdminAsync(services); // Perform seeding
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error seeding roles and admin user: {ex.Message}");
                    throw;
                }
            }

            // Middleware
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

        /// <summary>
        /// Seeds roles and an initial admin user.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            // Define roles
            string[] roles = { "Admin", "HR", "Lecturer" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var result = await roleManager.CreateAsync(new IdentityRole(role));
                    if (!result.Succeeded)
                    {
                        throw new Exception($"Failed to create role '{role}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }
                }
            }

            // Seed Admin User
            var adminEmail = "admin@email.com";
            var adminPassword = "Admin123!";
            await SeedUser(userManager, adminEmail, "Admin", adminPassword, "Admin");

            // Seed Lecturer User
            var lecturerEmail = "lecturer@email.com";
            var lecturerPassword = "Lecturer123!";
            await SeedUser(userManager, lecturerEmail, "Lecturer", lecturerPassword, "Lecturer");

            // Seed HR User
            var hrEmail = "hr@email.com";
            var hrPassword = "Human123!";
            await SeedUser(userManager, hrEmail, "HR", hrPassword, "HR");
        }

        private static async Task SeedUser(UserManager<IdentityUser> userManager, string email, string username, string password, string role)
        {
            var existingUser = await userManager.FindByEmailAsync(email);
            if (existingUser == null)
            {
                var newUser = new IdentityUser
                {
                    UserName = username,
                    Email = email,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(newUser, password);
                if (!result.Succeeded)
                {
                    throw new Exception($"Failed to create user '{username}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }

                var roleResult = await userManager.AddToRoleAsync(newUser, role);
                if (!roleResult.Succeeded)
                {
                    throw new Exception($"Failed to assign role '{role}' to user '{username}': {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
                }
            }
        }

    }
}
