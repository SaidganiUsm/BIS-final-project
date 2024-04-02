using FluentValidation;
using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces.Repositories;
using System.Drawing.Text;
using System.Runtime.CompilerServices;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Appointments.BaseValidators
{
	public class BaseAppointmentCommandValidator : AbstractValidator<IAppointmentCommandValidator>
	{
		private readonly int minimalAppoinementCreationDateDiff = 48;
		public BaseAppointmentCommandValidator() 
		{
			RuleFor(a => a.DoctorId)
				.MustAsync(
					async (currencyId, cancellationToken) =>
					{
						var currency = await _doctorRepository.GetAsync(
							c => c.Id == currencyId,
							cancellationToken: cancellationToken
						);
						if (currency == null)
							return false;

						return true;
					}
				)
				.Unless(a => a.DoctorId == null)
				.WithMessage("Currency entered does not exist in the system");

			ConfigureValidationWhenConcreteCreating();
		}

		private void ConfigureValidationWhenConcreteCreating()
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
