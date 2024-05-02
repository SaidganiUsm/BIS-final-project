using AutoMapper;
using OnlineAppointmentSchedulingSystem.Application.Features.Users.Commands.AssignDoctorRole;
using OnlineAppointmentSchedulingSystem.Application.Features.Users.Commands.Update;
using OnlineAppointmentSchedulingSystem.Application.Features.Users.Queries.GetById;
using OnlineAppointmentSchedulingSystem.Application.Features.Users.Queries.GetDoctors;
using OnlineAppointmentSchedulingSystem.Application.Features.Users.Queries.GetUserProfile;
using OnlineAppointmentSchedulingSystem.Core.Entities;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Users.Profiles
{
	public class MappingProfiles : Profile
	{
        public MappingProfiles()
        {
            CreateMap<GetDoctorsQueryResponse, User>().ReverseMap();
            CreateMap<UpdatedUserResponse, User>().ReverseMap();
			CreateMap<AssignDoctorRoleResponse, User>().ReverseMap();
            CreateMap<GetUserProfileResponse, User>().ReverseMap();
            CreateMap<GetByIdUserResponse, User>().ReverseMap();
        }
    }
}
