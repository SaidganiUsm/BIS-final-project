using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces.Repositories;
using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces;
using OnlineAppointmentSchedulingSystem.Core.Entities;
using OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Commands.Create;
using OnlineAppointmentSchedulingSystem.Core.Enums;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Staff.Commands.Create
{
    public class CreateAppoitnmentByStaffCommand : IRequest<CreateAppoitnmentByStaffCommandResponse>
    {
        public DateTime Date { get; set; }

        public int DoctorId { get; set; }

        public int ClientId { get; set; }
    }

    public class CreateAppoitnmentByStaffCommandHandler
        : IRequestHandler<CreateAppoitnmentByStaffCommand, CreateAppoitnmentByStaffCommandResponse>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IAppointmentStatusRepository _statusRepository;
        private readonly UserManager<User> _userManager;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        public CreateAppoitnmentByStaffCommandHandler(IAppointmentRepository appointmentRepository,
            IAppointmentStatusRepository appointmentStatusRepository,
            UserManager<User> userManager,
            ICurrentUserService currentUserService,
            IMapper mapper
        )
        {
            _currentUserService = currentUserService;
            _appointmentRepository = appointmentRepository;
            _statusRepository = appointmentStatusRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<CreateAppoitnmentByStaffCommandResponse>
            Handle(CreateAppoitnmentByStaffCommand request, CancellationToken cancellationToken)
        {
            AppointmentStatusEnum status = AppointmentStatusEnum.Approved;

            var appointmentStatus = await _statusRepository.GetAsync(
                s => s.Name == status.ToString(),
                cancellationToken: cancellationToken
            );

            var appointment = new Appointment
            {
                ClientId = request.ClientId,
                Date = request.Date,
                DoctorId = request.DoctorId,
                LocationId = 1,
                AppointmentStatus = appointmentStatus
            };

            var createdAppointment = await _appointmentRepository.AddAsync(appointment);

            var mappedAppointment = _mapper.Map<CreateAppoitnmentByStaffCommandResponse>(createdAppointment);

            return mappedAppointment;
        }
    }
}
