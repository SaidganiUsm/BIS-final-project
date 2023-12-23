using OnlineAppointmentSchedulingSystem.Core.Common;

namespace OnlineAppointmentSchedulingSystem.Core.Entities
{
	public class Appointment : BaseAuditableEntity
	{
		public DateTime Date { get; set; }

		public int ClientId { get; set; }

		public User User { get; set; }

		public virtual ICollection<Rate> Rates { get; set; }
	
		public string? Result { get; set; }
	}
}
