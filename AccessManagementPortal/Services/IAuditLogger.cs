namespace AccessManagementPortal.Services
{
    public interface IAuditLogger
    {

        Task LogAsync(string action, string entityType, int entityId, string? actorUserId = null, string? actorEmail = null);

    }
}
