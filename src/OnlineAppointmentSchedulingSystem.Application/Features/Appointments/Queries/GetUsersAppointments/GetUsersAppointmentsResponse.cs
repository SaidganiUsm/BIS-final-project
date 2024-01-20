using OnlineAppointmentSchedulingSystem.Application.Common.DTOs;
using OnlineAppointmentSchedulingSystem.Core.Entities;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Queries.GetUserAppointments
{
	public class GetUsersAppointmentsResponse
	{
		public int Id { get; set; }

		public DateTime Date { get; set; }

		public int DoctorId { get; set; }

		public UserDto Doctor { get; set; }
	}
}
