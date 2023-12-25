using OnlineAppointmentSchedulingSystem.Core.Common;

namespace OnlineAppointmentSchedulingSystem.Core.Entities
{
	public class Rate : BaseAuditableEntity
	{
		public int ClientId { get; set; }

		public virtual User Client { get; set; }

		public int DoctorId { get; set; }

		public User Doctor { get; set; }

		public byte RatingValue { get; set; }

		public string Comment { get; set; }

		public virtual Appointment Appointment { get; set; }

		public int AppointmentId { get; set; }
	}
}
