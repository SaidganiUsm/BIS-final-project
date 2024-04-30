using OnlineAppointmentSchedulingSystem.Application.Common.DTOs;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Patient.Queries.GetAllUpcomingAppointments
{
	public class GetUpcomingAppointmentResponse
	{
		public int Id { get; set; }

		public DateTime Date { get; set; }

		public int DoctorId { get; set; }

		public UserDto Doctor { get; set; }

		public int AppointmentStatusId { get; set; }

		public virtual AppointmentStatusDto AppointmentStatus { get; set; }
	}
}
