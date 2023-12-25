using OnlineAppointmentSchedulingSystem.Core.Common;

namespace OnlineAppointmentSchedulingSystem.Core.Entities
{
	public class AppointmentStatus : BaseAuditableEntity
	{
		public string Name { get; set; }

		public virtual ICollection<Appointment> Appointments { get; set; }
	}
}
