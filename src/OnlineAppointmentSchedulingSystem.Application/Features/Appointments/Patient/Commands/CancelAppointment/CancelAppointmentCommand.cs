using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces;
using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces.Repositories;
using OnlineAppointmentSchedulingSystem.Core.Entities;
using OnlineAppointmentSchedulingSystem.Core.Enums;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Patient.Commands.CancelAppointment
{
	public class CancelAppointmentCommand : IRequest<CancelAppointmentResponse>
	{
		public int Id { get; set; }
	}

	public class CancelAppointmentCommandHandler : IRequestHandler<CancelAppointmentCommand, CancelAppointmentResponse>
	{
		private readonly IAppointmentRepository _repository;
		private readonly IAppointmentStatusRepository _statusRepository;
		private readonly UserManager<User> _userManager;
		private readonly ICurrentUserService _currentUserService;

        public CancelAppointmentCommandHandler(
			IAppointmentRepository appointmentRepository,
			IAppointmentStatusRepository appointmentStatusRepository,
			UserManager<User> userManager,
			ICurrentUserService currentUserService
		)
        {
			_repository = appointmentRepository;
			_statusRepository = appointmentStatusRepository;
			_userManager = userManager;
			_currentUserService = currentUserService;            
        }

        public async Task<CancelAppointmentResponse> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
		{
			var cancelledStatus = AppointmentStatusEnum.Cancelled;

			var user = await _userManager.FindByEmailAsync(_currentUserService.UserEmail!);

			if ( user == null )
			{
				throw new ValidationException("User not found!");
			}

			var status = await _statusRepository.GetAsync(
				predicate: s => s.Name == cancelledStatus.ToString(),
				cancellationToken: cancellationToken
			);

			var appointment = await _repository.GetAsync(
				predicate: a => a.Id == request.Id,
				include: a => a.Include(s => s.AppointmentStatus!),
				cancellationToken: cancellationToken
			);

			if ( appointment == null )
			{
				throw new ValidationException("Appointment not found!");
			}

			appointment.AppointmentStatus = status;

			await _repository.UpdateAsync(appointment);

			return new CancelAppointmentResponse { Id = request.Id };
		}
	}
}
