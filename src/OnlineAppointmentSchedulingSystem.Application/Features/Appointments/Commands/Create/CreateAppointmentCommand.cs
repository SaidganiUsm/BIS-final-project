using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces;
using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces.Repositories;
using OnlineAppointmentSchedulingSystem.Application.Features.Appointments.BaseValidators;
using OnlineAppointmentSchedulingSystem.Core.Entities;
using OnlineAppointmentSchedulingSystem.Core.Enums;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Commands.Create
{
	public class CreateAppointmentCommand : IRequest<CreateAppointmentResponse>, IAppointmentCommandValidator
	{
		public DateTime Date { get; set; }

		public int DoctorId { get; set; }
	}

	public class CreateAppointmentCommandHandler 
		: IRequestHandler<CreateAppointmentCommand, CreateAppointmentResponse>
	{

		private readonly IAppointmentRepository _appointmentRepository;
		private readonly IAppointmentStatusRepository _statusRepository;
		private readonly UserManager<User> _userManager;
		private readonly ICurrentUserService _currentUserService;
		private readonly IMapper _mapper;

		public CreateAppointmentCommandHandler(
			IAppointmentRepository appointmentRepository,
			IAppointmentStatusRepository appointmentStatusRepository,
			UserManager<User> userManager,
			ICurrentUserService currentUserService,
			Mapper mapper
		)
		{
			_currentUserService = currentUserService;
			_appointmentRepository = appointmentRepository;
			_statusRepository = appointmentStatusRepository;
			_userManager = userManager;
			_mapper = mapper;
		}

		public async Task<CreateAppointmentResponse> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
		{
			AppointmentStatusEnum status = AppointmentStatusEnum.PendingApproval;

			var appointmentStatus = await _statusRepository.GetAsync(
				s => s.Name == status.ToString(),
				cancellationToken: cancellationToken
			);

			var user = await _userManager.FindByEmailAsync(_currentUserService.UserEmail!);

			var appointment = new Appointment
			{
				ClientId = user.Id,
				Date = request.Date,
				DoctorId = request.DoctorId,
				LocationId = 1,
			};

			var createdAppointment = await _appointmentRepository.AddAsync( appointment );

			var mappedAppointment = _mapper.Map<CreateAppointmentResponse>( createdAppointment );

			return mappedAppointment; 
		}
	}
}
