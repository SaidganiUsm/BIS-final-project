using OnlineAppointmentSchedulingSystem.Core.Common;

namespace OnlineAppointmentSchedulingSystem.Core.Entities
{
	public class Appointment : BaseAuditableEntity
	{
		public DateTime Date { get; set; }

		public int ClientId { get; set; }

		public User User { get; set; }

		public int? RateId { get; set; }

		//Rate id link
	
		public string? Result { get; set; }
	}
}
