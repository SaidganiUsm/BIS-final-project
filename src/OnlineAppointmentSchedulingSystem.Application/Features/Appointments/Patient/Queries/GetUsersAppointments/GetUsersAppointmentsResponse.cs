using OnlineAppointmentSchedulingSystem.Application.Common.DTOs;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Patient.Queries.GetUsersAppointments
{
    public class GetUsersAppointmentsResponse
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int DoctorId { get; set; }

        public UserDto Doctor { get; set; }

		public int AppointmentStatusId { get; set; }

		public virtual AppointmentStatusDto AppointmentStatus { get; set; }
	}
}
