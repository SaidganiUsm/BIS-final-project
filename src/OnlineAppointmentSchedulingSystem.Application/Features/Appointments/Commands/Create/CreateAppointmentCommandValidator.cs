using FluentValidation;
using OnlineAppointmentSchedulingSystem.Application.Features.Appointments.BaseValidators;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Commands.Create
{
	public class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
	{
		public CreateAppointmentCommandValidator()
		{
			Include(new BaseAppointmentCommandValidator());

			RuleFor(a => a.DoctorId).NotEmpty();
			RuleFor(a => a.Date).NotEmpty();
		}
	}
}
