namespace OnlineAppointmentSchedulingSystem.Core.Common
{
    public class BaseAuditableEntity : BaseEntity
    {
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
