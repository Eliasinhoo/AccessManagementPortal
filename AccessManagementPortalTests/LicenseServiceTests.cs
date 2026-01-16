using AccessManagementPortal.Data;
using AccessManagementPortal.Models;
using AccessManagementPortal.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace AccessManagementPortalTests
{
    public class LicenseServiceTests
    {
        [Fact]
        public async Task ToggleActiveAsync_TogglesFlag()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("ToggleActiveDb")
                .Options;

            using var context = new ApplicationDbContext(options);
            context.Licenses.Add(new License { Id = 1, IsActive = false });
            await context.SaveChangesAsync();

            var service = new LicenseService(context);

            await service.(1);

            var license = await context.Licenses.FindAsync(1);
            Assert.True(license!.IsActive);
        }
    }
}
