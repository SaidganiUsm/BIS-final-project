using AutoMapper;
using OnlineAppointmentSchedulingSystem.Application.Features.Staff.Commands.Create;
using OnlineAppointmentSchedulingSystem.Core.Entities;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Staff.Profiles
{
	public class MappingProfile : Profile
	{
		public MappingProfile() 
		{
			CreateMap<Appointment, CreateAppoitnmentByStaffCommandResponse>().ReverseMap();
		}
	}
}
