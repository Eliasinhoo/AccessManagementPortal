using AccessManagementPortal.Data;
using AccessManagementPortal.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccessManagementPortal.Controllers
{
    
    [Authorize(Roles="Admin")]
    public class LicensesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ILicenseService _licenseService;

        public LicensesController(ApplicationDbContext db, ILicenseService licenseService)
        {
            _db = db;
            _licenseService = licenseService;
        }

        public async Task<IActionResult> Index(bool? isActive, int? productId)
        {
            var q = new LicenseQuery(isActive, productId);

            var licenses = await _licenseService.GetLicensesAsync(q);

            //var licenses = await _db.Licensesc
            //    .Include(l => l.Product)
            //    .Include(l => l.ApplicationUser)
            //    .OrderByDescending(l => l.Product)
            //    .ToListAsync();

            return View(licenses);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var License = await _db.Licenses.FindAsync(id);

            if (License == null)
            {
                return NotFound();
            }

            _db.Licenses.Remove(License);

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");

        }


    }
}
