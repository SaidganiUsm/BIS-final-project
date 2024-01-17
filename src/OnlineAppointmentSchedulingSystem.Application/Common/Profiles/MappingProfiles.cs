using AutoMapper;
using OnlineAppointmentSchedulingSystem.Application.Common.DTOs;
using OnlineAppointmentSchedulingSystem.Core.Entities;

namespace OnlineAppointmentSchedulingSystem.Application.Common.Profiles
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles() 
		{
			CreateMap<User, UserDto>();
			CreateMap<Appointment, AppointmentDto>();
			CreateMap<Category, CategoryDto>();
			CreateMap<AppointmentStatus, AppointmentStatusDto>();
		}
	}
}
