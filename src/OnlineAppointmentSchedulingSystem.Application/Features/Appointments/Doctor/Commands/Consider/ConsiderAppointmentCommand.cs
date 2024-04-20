using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces.Repositories;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Doctor.Commands.Consider
{
	public class ConsiderAppointmentCommand : IRequest<ConsiderAppointmentCommandResponse>
	{
		public int Id { get; set; }
		public string AppointmentStatus { get; set; }
	}

	public class ConsiderAppointmentCommandHandler : IRequestHandler<ConsiderAppointmentCommand, ConsiderAppointmentCommandResponse>
	{
		private readonly IAppointmentRepository _appointmentRepository;
		private readonly IAppointmentStatusRepository _appointmentStatusRepository;
		private readonly IMapper _mapper;

        public ConsiderAppointmentCommandHandler(
			IAppointmentRepository appointmentRepository,
			IAppointmentStatusRepository appointmentStatusRepository,
			IMapper mapper
		)
        {
			_appointmentRepository = appointmentRepository;
			_appointmentStatusRepository = appointmentStatusRepository;
			_mapper = mapper;
        }

        public async Task<ConsiderAppointmentCommandResponse> 
			Handle(ConsiderAppointmentCommand request, CancellationToken cancellationToken)
		{
			var appointmentStatus = await _appointmentStatusRepository.GetAsync(
				s => s.Name == request.AppointmentStatus,
				cancellationToken: cancellationToken
			);

			if (appointmentStatus == null)
			{
				throw new ValidationException("Appointment status not found");
			}

			var appointment = await _appointmentRepository.GetAsync(predicate: x => x.Id == request.Id,
				include: a => a.Include(a => a.AppointmentStatus),
			cancellationToken: cancellationToken);

			if (appointment == null)
			{
				throw new ValidationException("Appointment not found");
			}

			appointment.AppointmentStatus = appointmentStatus;

			await _appointmentRepository.UpdateAsync(appointment);

			var result = new ConsiderAppointmentCommandResponse
			{
				Id = appointment.Id
			};

			var mappedResult = _mapper.Map<ConsiderAppointmentCommandResponse>(result);

			return mappedResult;
		}
	}
}
