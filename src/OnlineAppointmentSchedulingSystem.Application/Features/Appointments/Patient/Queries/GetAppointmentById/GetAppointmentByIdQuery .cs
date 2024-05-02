using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces.Repositories;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Patient.Queries.GetAppointmentById
{
    public class GetAppointmentByIdQuery : IRequest<GetAppointmentByIdResponse>
    {
        public int Id { get; set; }
    }

    public class GetAppointmentByIdQueryHandler : IRequestHandler<GetAppointmentByIdQuery, GetAppointmentByIdResponse>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;

        public GetAppointmentByIdQueryHandler(IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }

        public async Task<GetAppointmentByIdResponse> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetAsync(predicate: x => x.Id == request.Id,
                include: a => a
                .Include(a => a.Client)
                .Include(a => a.Doctor)
                .Include(a => a.AppointmentStatus),
            cancellationToken: cancellationToken);

            if (appointment == null)
            {
                throw new ValidationException("Appointment not found");
            }

            var appointmentDto = _mapper.Map<GetAppointmentByIdResponse>(appointment);

            return appointmentDto;
        }
    }
}
