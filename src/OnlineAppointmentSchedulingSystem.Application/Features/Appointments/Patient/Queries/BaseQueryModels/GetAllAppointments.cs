using OnlineAppointmentSchedulingSystem.Application.Common.DTOs;
using OnlineAppointmentSchedulingSystem.Core.Entities;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Patient.Queries.BaseQueryModels
{
    public class GetAllAppointments
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int ClientId { get; set; }

        public UserDto Client { get; set; }

        public int DoctorId { get; set; }

        public UserDto Doctor { get; set; }

        public virtual AppointmentStatusDto AppointmentStatus { get; set; }

        public string? Result { get; set; }
    }
}
