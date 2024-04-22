using AutoMapper;
using OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Staff.Commands.Create;
using OnlineAppointmentSchedulingSystem.Core.Entities;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Staff.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Appointment, CreateAppoitnmentByStaffCommandResponse>().ReverseMap();
        }
    }
}
