using AutoMapper;
using OnlineAppointmentSchedulingSystem.Application.Features.Users.Commands.AssignDoctorRole;
using OnlineAppointmentSchedulingSystem.Application.Features.Users.Queries.GetDoctors;
using OnlineAppointmentSchedulingSystem.Core.Entities;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Users.Profiles
{
	public class MappingProfiles : Profile
	{
        public MappingProfiles()
        {
            CreateMap<GetDoctorsQueryResponse, User>().ReverseMap();

			CreateMap<AssignDoctorRoleCommandResponse, User>().ReverseMap();
        }
    }
}
