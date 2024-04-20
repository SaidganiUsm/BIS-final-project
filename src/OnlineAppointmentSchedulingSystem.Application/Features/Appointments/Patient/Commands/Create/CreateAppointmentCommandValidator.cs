using FluentValidation;
using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces.Repositories;
using OnlineAppointmentSchedulingSystem.Application.Features.Appointments.BaseValidators;
using OnlineAppointmentSchedulingSystem.Core.Enums;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Commands.Create
{
	public class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
	{
		private readonly int minimalAppoinementCreationDateDiff = 48;
		public CreateAppointmentCommandValidator(IAppointmentRepository appointmentRepository)
		{
			RuleFor(a => a.DoctorId).NotEmpty();

			RuleFor(a => a.Date).NotEmpty();

			RuleFor(l => l.Date)
				.NotEmpty()
				.Must(time =>
				{
					if (time > DateTime.UtcNow)
					{
						var timeDifference = time - DateTime.UtcNow;

						if (timeDifference.TotalHours >= minimalAppoinementCreationDateDiff)
							return true;
					}

					return false;
				})
				.WithMessage("Appointment date should be al least 48 hours before");

			RuleFor(a => a.Date)
				.Must(BeDivisibleByTenMinutes)
				.WithMessage("Appointment time must be divisible by 10 minutes.");

			RuleFor(a => a.Date)
				.Must(date => !IsWeekend(date))
				.WithMessage("Appointments cannot be scheduled on Saturdays or Sundays.");


			RuleFor(a => a.Date)
				.MustAsync(async (date, cancellation) =>
				{
					var existingAppointments = await appointmentRepository.GetUnpaginatedListAsync(
						predicate: a => a.Date == date && 
						a.AppointmentStatus.Name == AppointmentStatusEnum.Approved.ToString() &&
						a.AppointmentStatus.Name == AppointmentStatusEnum.PendingApproval.ToString(),
						enableTracking: false);


					return existingAppointments == null || !existingAppointments.Any();
				})
				.WithMessage("An appointment already exists at this time.");
		}

		private bool BeDivisibleByTenMinutes(DateTime date)
		{
			return date.Minute % 10 == 0;
		}

		private bool IsWeekend(DateTime date)
		{
			return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
		}
	}
}
