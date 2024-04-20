using FluentValidation;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Appointments.BaseValidators
{
	public class BaseAppointmentCommandValidator : AbstractValidator<IAppointmentCommandValidator>
	{
		private readonly int minimalAppoinementCreationDateDiff = 48;
		public BaseAppointmentCommandValidator()
		{
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
		}
	}
}
