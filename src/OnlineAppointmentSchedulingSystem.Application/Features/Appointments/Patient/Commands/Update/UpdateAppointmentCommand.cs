using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces;
using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces.Repositories;
using OnlineAppointmentSchedulingSystem.Core.Entities;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Commands.Update
{
	public class UpdateAppointmentCommand : IRequest<UpdateAppointmentResponse>
	{
		public int AppointmentId { get; set; }
		public DateTime Date { get; set; }
	}

	public class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand, UpdateAppointmentResponse>
	{
		private readonly IAppointmentRepository _appointmentRepository;
		private readonly UserManager<User> _userManager;
		private readonly ICurrentUserService _currentUserService;
		private readonly IMapper _mapper;

		public UpdateAppointmentCommandHandler(
			IAppointmentRepository appointmentRepository, 
			UserManager<User> userManager,
			ICurrentUserService currentUserService,
			IMapper mapper
		)
		{
			_appointmentRepository = appointmentRepository;
			_userManager = userManager;
			_currentUserService = currentUserService;
			_mapper = mapper;
		}

		public async Task<UpdateAppointmentResponse> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByEmailAsync(_currentUserService.UserEmail!);

			var userAppointment = await _appointmentRepository.GetAsync(
				predicate: a => a.ClientId == user.Id,
				include: a => a.Include(u => u.Doctor),
				enableTracking: false,
				cancellationToken: cancellationToken
				);

			if (userAppointment == null)
			{
				return null;
			}

			userAppointment.Date = request.Date;

			await _appointmentRepository.UpdateAsync(userAppointment);

			var result = new UpdateAppointmentResponse
			{
				Id = userAppointment.Id,
				Date = userAppointment.Date
			};

			var mappedResult = _mapper.Map<UpdateAppointmentResponse>(result);

			return mappedResult;
		}
	}
}
