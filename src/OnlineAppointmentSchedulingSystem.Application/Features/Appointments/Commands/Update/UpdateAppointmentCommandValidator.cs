using FluentValidation;
using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces.Repositories;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Commands.Update
{
	public class UpdateAppointmentCommandValidator : AbstractValidator<UpdateAppointmentCommand>
	{
		private readonly IAppointmentRepository _appointmentRepository;

		public UpdateAppointmentCommandValidator(
			IAppointmentRepository appointmentRepository,
			IAppointmentStatusRepository _appointmentStatusRepository
		)
		{
			_appointmentRepository = appointmentRepository;

		}
	}
}
