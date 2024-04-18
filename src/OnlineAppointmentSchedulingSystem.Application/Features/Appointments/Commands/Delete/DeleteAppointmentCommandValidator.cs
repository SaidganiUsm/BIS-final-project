using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces;
using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces.Repositories;
using OnlineAppointmentSchedulingSystem.Core.Entities;
using System.Threading;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Commands.Delete
{

	public class DeleteAppointmentCommandValidator : AbstractValidator<DeleteAppointmentCommand>
	{
		private readonly IAppointmentRepository _appointmentRepository;
		private readonly ICurrentUserService _currentUserService;
		private readonly UserManager<User> _userManager;

		public DeleteAppointmentCommandValidator(
			IAppointmentRepository appointmentRepository,
			ICurrentUserService currentUserService,
			UserManager<User> userManager
		)
		{
			_appointmentRepository = appointmentRepository;
			_currentUserService = currentUserService;
			_userManager = userManager;

			RuleFor(a => a.Id)
				.MustAsync(
					async (id, cancellationToken) =>
					{
						var appointment = await _appointmentRepository.GetAsync(
							predicate: a => a.Id == id,
							cancellationToken: cancellationToken
						);

						return appointment != null;
					}
				)
				.WithMessage("Appointment with this Id does not exist");

			RuleFor(a => a.Id)
				.MustAsync(
					async (id, cancellationToken) =>
					{
						var user = await _userManager.Users.FirstOrDefaultAsync(
							u => u.Email == _currentUserService.UserEmail!,
							cancellationToken: cancellationToken
						);

						var appointment = await _appointmentRepository.GetAsync(
							predicate: a => a.Id == id,
							cancellationToken: cancellationToken
						);

						return appointment!.ClientId == user!.Id;
					}
				)
				.WithMessage("You can delete only your own appointment");

			RuleFor(a => a.Id)
					.MustAsync(async (id, cancellationToken) =>
					{
						var appointment = await _appointmentRepository.GetAsync(
							predicate: a => a.Id == id,
							cancellationToken: cancellationToken
						);

						return (appointment.Date - DateTime.Today).TotalDays >= 1;
					}
				)
				.WithMessage("You can delete the appointment only if there is at least one day left");
		}
	}
}
