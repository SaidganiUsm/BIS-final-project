using OnlineAppointmentSchedulingSystem.Application.Common.DTOs;
using OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Doctor.Queries.BaseQueryModels;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Patient.Queries.GetAllClientAppointments
{
    public class GetAllClinetAppointmentsResponse
    {
		public int Id { get; set; }

		public DateTime Date { get; set; }

		public int ClientId { get; set; }

		public UserDto Client { get; set; }

		public int DoctorId { get; set; }

		public int AppointmentStatusId { get; set; }

		public virtual AppointmentStatusDto AppointmentStatus { get; set; }
	}
}
