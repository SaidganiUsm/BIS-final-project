using Microsoft.AspNetCore.Identity;

namespace OnlineAppointmentSchedulingSystem.Core.Entities
{
	public class User : IdentityUser<int>
	{
		public string? FirstName { get; set; }

		public string? LastName { get; set; }

		public virtual ICollection<Appointment> DoctorAppointments { get; set; }

		public virtual ICollection<Appointment> ClientAppointments { get; set; }

		public virtual ICollection<Rate> SenderRates { get; set; }

		public virtual ICollection<Rate> ReceiverRates { get; set; }
	}
}
