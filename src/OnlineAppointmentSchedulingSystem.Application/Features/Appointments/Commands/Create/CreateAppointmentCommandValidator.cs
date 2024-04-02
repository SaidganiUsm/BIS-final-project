using FluentValidation;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Commands.Create
{
	public class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
	{
		public CreateAppointmentCommandValidator()
		{
		}
	}
}
