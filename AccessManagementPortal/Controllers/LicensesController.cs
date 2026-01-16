using AccessManagementPortal.Data;
using AccessManagementPortal.Services;
using AccessManagementPortal.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccessManagementPortal.Controllers
{
    
    [Authorize(Roles="Admin")]
    public class LicensesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IAuditLogger _AuditLogger;
        private readonly ILicenseService _licenseService;

        public LicensesController(ApplicationDbContext db, IAuditLogger AuditLogger, ILicenseService licenseService)
        {
            _db = db;
            _licenseService = licenseService;
            _AuditLogger = AuditLogger;

        }

        public async Task<IActionResult> Index(bool? isActive, int? productId)
        {
            var q = new LicenseQuery(isActive, productId);

            var vm = new LicensesIndexVm
            {
                IsActive = isActive,
                ProductId = productId,
                Licenses = await _licenseService.GetLicensesAsync(q),
                Products = await _db.Products.OrderBy(p => p.Name).ToListAsync()
            };


            return View(vm);
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
            // Audit logging
            await _AuditLogger.LogAsync(
                action: "DeleteLicense",
                entityType: "License",
                entityId: id,
                actorUserId: User.Identity.Name,
                actorEmail: User.Identity.Name);

            return RedirectToAction("Index");

            
        }

        [HttpPost]
        public async Task<IActionResult> ToggleActive(int id)
        {
            var License = await _licenseService.ToggleLicenseAsync(id);

            // Audit logging
            await _AuditLogger.LogAsync(
                action: "ToggleLicenseActive",
                entityType: "License",
                entityId: License.Id,
                actorUserId: User.Identity.Name,
                actorEmail: User.Identity.Name);


            return RedirectToAction("Index");
        }


    }
}
