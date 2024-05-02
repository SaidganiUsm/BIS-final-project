using AutoMapper;
using OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Patient.Queries.GetAllClientAppointments;
using OnlineAppointmentSchedulingSystem.Core.Entities;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Doctor.Profiles
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles() 
		{
			CreateMap<Appointment, GetAllClinetAppointmentsResponse>().ReverseMap();
		}
	}
}
