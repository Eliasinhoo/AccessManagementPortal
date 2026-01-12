using AccessManagementPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AccessManagementPortal.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            context.Database.Migrate();
            //context.Database.EnsureCreated();

            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                roleManager.CreateAsync(new IdentityRole("Admin")).Wait();
            }

            if (!roleManager.RoleExistsAsync("User").Result)
            {
                roleManager.CreateAsync(new IdentityRole("User")).Wait();
            }

            var adminEmail = "admin@portal.com";
            var adminUser = userManager.FindByEmailAsync(adminEmail).Result;

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "System",
                    LastName = "Admin",
                    EmailConfirmed = true
                };

                userManager.CreateAsync(adminUser, "Admin123!").Wait();
                userManager.AddToRoleAsync(adminUser, "Admin").Wait();
            }

            var userEmail = "user@portal.com";
            var testUser = userManager.FindByEmailAsync(userEmail).Result;
            if (testUser == null)
            {
                testUser = new ApplicationUser
                {
                    UserName = userEmail,
                    Email = userEmail,
                    FirstName = "Test",
                    LastName = "User",
                    EmailConfirmed = true
                };
                userManager.CreateAsync(testUser, "User123!").Wait();
                userManager.AddToRoleAsync(testUser, "User").Wait();
            }

            if (!context.Products.Any())
            {
                Console.WriteLine("Seeding products...");

                context.Products.AddRange(
                    new Product
                    {
                        Name = "Analytics Dashboard",
                        Description = "Advanced reporting and analytics tools"
                    },
                    new Product
                    {
                        Name = "API Access",
                        Description = "Programmatic access to platform features"
                    },
                    new Product
                    {
                        Name = "User Management",
                        Description = "Manage users and permissions"
                    }
                );

                context.SaveChanges();
            }

            var analytics = context.Products.First(p => p.Name == "Analytics Dashboard");
            var api = context.Products.First(p => p.Name == "Api Access");

            bool HasLicense(string userId, int productId) =>
                context.Licenses.Any(l => l.ApplicationUserId == userId && l.ProductId == productId);

            if (!HasLicense(testUser.Id, analytics.Id))
            {
                context.Licenses.Add(new License
                {
                    ApplicationUserId = testUser.Id,
                    ApplicationUser = testUser,

                    ProductId = analytics.Id,
                    Product = analytics,
                    LicenseType = LicenseType.Free,

                    IsActive = true,
                    AssignedDate = DateTime.UtcNow.Date,
                    ExpiryDate = DateTime.UtcNow.Date.AddMonths(1)
                });
            }

            if (!HasLicense(testUser.Id, api.Id))
            {
                context.Licenses.Add(new License
                {
                    ApplicationUserId = testUser.Id,
                    ApplicationUser = testUser,

                    ProductId = api.Id,
                    Product = api,
                    LicenseType = LicenseType.Free,

                    IsActive = true,
                    AssignedDate = DateTime.UtcNow.Date,
                    ExpiryDate = DateTime.UtcNow.Date.AddMonths(1)
                });
            }

            context.SaveChanges();
        }
    }
}
