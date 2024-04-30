using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces;
using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces.Repositories;
using OnlineAppointmentSchedulingSystem.Core.Entities;
using OnlineAppointmentSchedulingSystem.Core.Enums;
using System.Runtime.CompilerServices;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Doctor.Commands.ConsiderAllAppointments
{
	public class ConsiderAllAppointmentsCommand : IRequest<ConsiderAllAppointmentsResponse>
	{
		public DateTime? DateTime { get; set; }
	}

	public class ConsiderAllAppointmentsCommandHandler : IRequestHandler<ConsiderAllAppointmentsCommand, ConsiderAllAppointmentsResponse>
	{
		private readonly IAppointmentRepository _appointmentRepository;
		private readonly IAppointmentStatusRepository _appointmentStatusRepository;
		private readonly UserManager<User> _userManager;
		private readonly ICurrentUserService _currentUserService;

		public ConsiderAllAppointmentsCommandHandler(
			IAppointmentRepository appointmentRepository, 
			IAppointmentStatusRepository appointmentStatusRepository,
			UserManager<User> userManager,
			ICurrentUserService currentUserService
		)
        {
			_appointmentRepository = appointmentRepository;
			_appointmentStatusRepository = appointmentStatusRepository;
			_userManager = userManager;
			_currentUserService = currentUserService;
		}
        async Task<ConsiderAllAppointmentsResponse> IRequestHandler<ConsiderAllAppointmentsCommand, ConsiderAllAppointmentsResponse>.Handle(ConsiderAllAppointmentsCommand request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByEmailAsync(_currentUserService.UserEmail!);

			var penddingApproval = AppointmentStatusEnum.PendingApproval;
			var approved = AppointmentStatusEnum.Approved;

			DateTime queryDate = request.DateTime ?? DateTime.Today;
			var startDate = queryDate.Date;
			var endDate = startDate.AddDays(1).AddTicks(-1);

			var userAppointments = await _appointmentRepository.GetUnpaginatedListAsync(
				predicate: a => a.DoctorId == user.Id 
					&& a.Date >= startDate && a.Date <= endDate 
					&& a.AppointmentStatus!.Name == penddingApproval.ToString(),
				include: a => a.Include(s => s.AppointmentStatus),
				enableTracking: false,
				cancellationToken: cancellationToken
			);

			var newStatus = await _appointmentStatusRepository.GetAsync(
				predicate: a => a.Name == approved.ToString(), 
				cancellationToken: cancellationToken
			);

			foreach (var appointment in userAppointments)
			{
				appointment.AppointmentStatus = newStatus;
				await _appointmentRepository.UpdateAsync(appointment);
			}

			return new ConsiderAllAppointmentsResponse
			{
				Success = true,
			};
		}
	}
}
