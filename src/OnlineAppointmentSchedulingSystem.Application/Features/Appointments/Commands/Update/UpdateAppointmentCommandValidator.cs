using FluentValidation;
using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces.Repositories;
using OnlineAppointmentSchedulingSystem.Core.Enums;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Commands.Update
{
	public class UpdateAppointmentCommandValidator : AbstractValidator<UpdateAppointmentCommand>
	{
		public UpdateAppointmentCommandValidator(IAppointmentRepository _appointmentRepository)
		{
			RuleFor(a => a.AppointmentId).NotEmpty();
			RuleFor(a => a.Date).NotEmpty();

			RuleFor(a => a.Date)
				.Must(BeOneDayLeft)
				.WithMessage("Update can only be performed if there is one day left to the appointment.");
			RuleFor(a => a.AppointmentId)
				.MustAsync(async (id, cancellationToken) =>
				{
					var appointment = await _appointmentRepository.GetAsync(predicate: a => a.Id == id, 
						cancellationToken: cancellationToken);
					return appointment != null && appointment.AppointmentStatus.Name != AppointmentStatusEnum.Approved.ToString();
				}
			)
			.WithMessage("Approved appointments cannot be updated.");
		}

		private bool BeOneDayLeft(DateTime date)
		{
			return (date - DateTime.Today).Days == 1;
		}
	}
}
