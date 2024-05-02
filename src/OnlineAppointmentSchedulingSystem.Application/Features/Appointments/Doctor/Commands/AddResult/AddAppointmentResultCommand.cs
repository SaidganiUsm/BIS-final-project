using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces.Repositories;
using OnlineAppointmentSchedulingSystem.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Doctor.Commands.AddResult
{
	public class AddAppointmentResultCommand : IRequest<AddAppointmentResultCommandResponse>
	{
		public int Id { get; set; }

		public string Result { get; set; }
	}

	public class AddAppointmentResultCommandHandler
		: IRequestHandler<AddAppointmentResultCommand, AddAppointmentResultCommandResponse>
	{
		private readonly IAppointmentRepository _appointmentRepository;
		private readonly IAppointmentStatusRepository _appointmentStatusRepository;

        public AddAppointmentResultCommandHandler(
			IAppointmentRepository appointmentRepository,
			IAppointmentStatusRepository appointmentStatusRepository
		)
        {
            _appointmentRepository = appointmentRepository;
			_appointmentStatusRepository = appointmentStatusRepository;
        }
        public async Task<AddAppointmentResultCommandResponse> Handle(AddAppointmentResultCommand request, CancellationToken cancellationToken)
		{
			var appointment = await _appointmentRepository.GetAsync(
				predicate: a => a.Id == request.Id,
				include: a => a.Include(a => a.AppointmentStatus)
								.Include(a => a.Client),
				cancellationToken: cancellationToken
			);

			if ( appointment == null )
			{
				throw new ValidationException("Appointment not found!");
			}

			var statusName = AppointmentStatusEnum.Done;

			var status = await _appointmentStatusRepository.GetAsync(
				predicate: s => s.Name == statusName.ToString(),
				cancellationToken: cancellationToken
			);

			appointment.Result = request.Result;
			appointment.AppointmentStatus = status;

			await _appointmentRepository.UpdateAsync( appointment );

			return new AddAppointmentResultCommandResponse { Id = request.Id};
			
		}
	}
}
