using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces.Repositories;
using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces;
using OnlineAppointmentSchedulingSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using OnlineAppointmentSchedulingSystem.Core.Enums;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Commands.Delete
{
	public class DeleteAppointmentCommand : IRequest<DeleteAppointmentResponse>
	{
		public int Id { get; set; }
	}

	public class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand, DeleteAppointmentResponse>
	{
		private readonly IAppointmentRepository _appointmentRepository;
		private readonly IAppointmentStatusRepository _statusRepository;

		public DeleteAppointmentCommandHandler(
			IAppointmentRepository appointmentRepository,
			IAppointmentStatusRepository appointmentStatusRepository
		)
		{
			_appointmentRepository = appointmentRepository;
			_statusRepository = appointmentStatusRepository;
		}

		public async Task<DeleteAppointmentResponse> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
		{
			var appointment = await _appointmentRepository.GetAsync(
				predicate: x => x.Id == request.Id,
				include: c => c.Include(u => u.Client),
				cancellationToken: cancellationToken
			);

			var currentAppointmentStatus = appointment!.AppointmentStatus!.Name;

			if ( currentAppointmentStatus == AppointmentStatusEnum.PendingApproval.ToString() &&
				currentAppointmentStatus == AppointmentStatusEnum.Approved.ToString()
			)
			{
				var newLotStatus = await _statusRepository.GetAsync(
				predicate: s => s.Name == AppointmentStatusEnum.Cancelled.ToString(),
				cancellationToken: cancellationToken
				);

				appointment.AppointmentStatus = newLotStatus;

				await _appointmentRepository.UpdateAsync(appointment);
			}

			return new DeleteAppointmentResponse { Id = appointment.Id };
		}
	}
}
