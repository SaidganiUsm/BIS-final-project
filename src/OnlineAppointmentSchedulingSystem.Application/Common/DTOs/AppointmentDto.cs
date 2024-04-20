using OnlineAppointmentSchedulingSystem.Core.Entities;

namespace OnlineAppointmentSchedulingSystem.Application.Common.DTOs
{
	public class AppointmentDto
	{
		public int Id { get; set; }

		public DateTime Date { get; set; }

		public int ClientId { get; set; }

		public UserDto Client { get; set; }

		public int DoctorId { get; set; }

		public UserDto Doctor { get; set; }

		public int AppointmentStatusId { get; set; }

		public virtual AppointmentStatusDto AppointmentStatus { get; set; }

		public int LocationId { get; set; }

		public int? RateId { get; set; }

		public virtual Rate? Rate { get; set; }

		public string? Result { get; set; }
	}
}
