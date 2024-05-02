using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces;
using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces.Repositories;
using OnlineAppointmentSchedulingSystem.Core.Entities;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Patient.Queries.GetUsersAppointments
{
    public class GetUsersAppointmentsQuery : IRequest<List<GetUsersAppointmentsResponse>>
    {
        public DateTime? DateTime { get; set; }
    }

    public class GetUsersAppointmentsQueryHandler
        : IRequestHandler<GetUsersAppointmentsQuery, List<GetUsersAppointmentsResponse>>
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

        public async Task<List<GetUsersAppointmentsResponse>> Handle(
            GetUsersAppointmentsQuery request,
            CancellationToken cancellationToken
        )
        {
            var user = await _userManager.FindByEmailAsync(_currentUserService.UserEmail!);

			DateTime queryDate = request.DateTime ?? DateTime.Today;
			var startDate = queryDate.Date;
            var endDate = startDate.AddDays(1).AddTicks(-1);

			var userAppointments = await _appointmentRepository.GetUnpaginatedListAsync(
				predicate: a => a.ClientId == user.Id && a.Date >= startDate && a.Date <= endDate,
				include: a => a.Include(d => d.Doctor)
							.Include(s => s.AppointmentStatus),
				enableTracking: false,
				cancellationToken: cancellationToken
			);

            var response = _mapper.Map<List<GetUsersAppointmentsResponse>>(userAppointments);

            return response;
        }
    }
}
