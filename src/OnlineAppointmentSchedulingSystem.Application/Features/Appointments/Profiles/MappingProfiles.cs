using AutoMapper;
using OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Commands.Create;
using OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Commands.Delete;
using OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Commands.Update;
using OnlineAppointmentSchedulingSystem.Core.Entities;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Profiles
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles() 
		{
			CreateMap<Appointment, UpdateAppointmentResponse>().ReverseMap();
			CreateMap<Appointment, DeleteAppointmentResponse>().ReverseMap();
			CreateMap<Appointment, CreateAppointmentResponse>().ReverseMap();
		}
	}
}
