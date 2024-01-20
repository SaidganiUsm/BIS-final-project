using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineAppointmentSchedulingSystem.Application.Common.DTOs;
using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces;
using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces.Repositories;
using OnlineAppointmentSchedulingSystem.Application.Common.Models.Requests;
using OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Queries.GetUserAppointments;
using OnlineAppointmentSchedulingSystem.Core.Entities;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Queries.GetUsersAppointments
{
	public class GetUsersAppointmentsQuery : IRequest<GetListResponseDto<GetUsersAppointmentsResponse>>
	{
		public PageRequest PageRequest { get; set; }
	}

	public class GetUsersAppointmentsQueryHandler
		: IRequestHandler<GetUsersAppointmentsQuery, GetListResponseDto<GetUsersAppointmentsResponse>>
	{

		private readonly ICurrentUserService _currentUserService;
		private readonly UserManager<User> _userManager;
		private readonly IAppointmentRepository _appointmentRepository;
		private readonly IMapper _mapper;

		public GetUsersAppointmentsQueryHandler(
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

		public async Task<GetListResponseDto<GetUsersAppointmentsResponse>> Handle(
			GetUsersAppointmentsQuery request, 
			CancellationToken cancellationToken
		)
		{
			var user = await _userManager.FindByEmailAsync(_currentUserService.UserEmail!);

			var userAppointment = await _appointmentRepository.GetListAsync(
				predicate: a => a.ClientId == user.Id,
				include: a => a.Include(u => u.Doctor),
				enableTracking: false,
				size: request.PageRequest.PageSize,
				index: request.PageRequest.PageIndex,
				cancellationToken: cancellationToken
				);

			var response = _mapper.Map<GetListResponseDto<GetUsersAppointmentsResponse>>(userAppointment);

			return response;
		}
	}
}
