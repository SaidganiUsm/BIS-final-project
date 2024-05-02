using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces;
using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces.Repositories;
using OnlineAppointmentSchedulingSystem.Core.Entities;
using OnlineAppointmentSchedulingSystem.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Commands.Create
{
	public class CreateAppointmentCommand : IRequest<CreateAppointmentResponse>
	{
		private DateTime _date;

		public DateTime Date
		{
			get => _date;
			set => _date = new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, 0);
		}

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
			IMapper mapper
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

			if ( appointmentStatus == null )
			{
				throw new ValidationException("Status not found");
			}

			var user = await _userManager.FindByEmailAsync(_currentUserService.UserEmail!);

			var appointment = new Appointment
			{
				ClientId = user.Id,
				Date = request.Date,
				DoctorId = request.DoctorId,
				LocationId = 1,
				AppointmentStatus = appointmentStatus
			};

			var createdAppointment = await _appointmentRepository.AddAsync(appointment);

			var mappedAppointment = _mapper.Map<CreateAppointmentResponse>(createdAppointment);

			return mappedAppointment;
		}
	}
}
