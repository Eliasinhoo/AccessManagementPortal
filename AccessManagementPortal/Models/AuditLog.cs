namespace AccessManagementPortal.Models
{
    public class AuditLog
    {
        public int Id { get; set; }

        public DateTime UtcTimestamp { get; set; } = DateTime.UtcNow;

        public string? ActorUserId { get; set; }
        public string? ActorEmail { get; set; }

        public string Action { get; set; } = default!;
        public string EntityType { get; set; } = default!;
        public int EntityId { get; set; } = default!;

    }
}
