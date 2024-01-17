using OnlineAppointmentSchedulingSystem.Core.Entities;

namespace OnlineAppointmentSchedulingSystem.Application.Common.DTOs
{
	public class AppointmentDto
	{
		public int Id { get; set; }

		public DateTime Date { get; set; }

		public int ClientId { get; set; }

		public User Client { get; set; }

		public int DoctorId { get; set; }

		public User Doctor { get; set; }

		public int AppointmentStatusId { get; set; }

		public virtual AppointmentStatus AppointmentStatus { get; set; }

		public int LocationId { get; set; }

		public int? RateId { get; set; }

		public virtual Rate? Rate { get; set; }

		public string? Result { get; set; }
	}
}
