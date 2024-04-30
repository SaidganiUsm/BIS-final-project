using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces.Repositories;
using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces;
using OnlineAppointmentSchedulingSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Patient.Queries.GetAllUpcomingAppointments
{
	public class GetUpcomingAppointmentQuery : IRequest<List<GetUpcomingAppointmentResponse>>
	{
	}

	public class GetUpcomingAppointmentQueryHandler : IRequestHandler<GetUpcomingAppointmentQuery, List<GetUpcomingAppointmentResponse>>
	{
		private readonly ICurrentUserService _currentUserService;
		private readonly UserManager<User> _userManager;
		private readonly IAppointmentRepository _appointmentRepository;
		private readonly IMapper _mapper;

		public GetUpcomingAppointmentQueryHandler(
			ICurrentUserService currentUserService,
			UserManager<User> userManager,
			IAppointmentRepository appointmentRepository,
			IMapper mapper
		)
		{
			_currentUserService = currentUserService;
			_userManager = userManager;
			_appointmentRepository = appointmentRepository;
			_mapper = mapper;
		}

		public async Task<List<GetUpcomingAppointmentResponse>> Handle(GetUpcomingAppointmentQuery request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByEmailAsync(_currentUserService.UserEmail!);

			DateTime queryDate = DateTime.Today;

			var userAppointments = await _appointmentRepository.GetUnpaginatedListAsync(
				predicate: a => a.ClientId == user.Id && a.Date >= queryDate,
				include: a => a.Include(d => d.Doctor)
							.Include(s => s.AppointmentStatus),
				enableTracking: false,
				cancellationToken: cancellationToken
			);

			var response = _mapper.Map<List<GetUpcomingAppointmentResponse>>(userAppointments);

			return response;
		}
	}
}
