using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces;
using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces.Repositories;
using OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Patient.Queries.GetAllClientAppointments;
using OnlineAppointmentSchedulingSystem.Core.Entities;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Doctor.Queries.GetAllClientAppointments
{
    public class GetAllClinetAppointmentsQuery : IRequest<List<GetAllClinetAppointmentsResponse>>
    {
		public DateTime? DateTime { get; set; }
	}

    public class GetAllClinetAppointmentsQueryHandler
        : IRequestHandler<GetAllClinetAppointmentsQuery, List<GetAllClinetAppointmentsResponse>>
    {
        private readonly ICurrentUserService _currentUserService;
		private readonly UserManager<User> _userManager;
		private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;

        public GetAllClinetAppointmentsQueryHandler(
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

        public async Task<List<GetAllClinetAppointmentsResponse>> Handle(
		    GetAllClinetAppointmentsQuery request,
            CancellationToken cancellationToken
        )
        {
			var user = await _userManager.FindByEmailAsync(_currentUserService.UserEmail!);

			DateTime queryDate = request.DateTime ?? DateTime.Today;
			var startDate = queryDate.Date;
			var endDate = startDate.AddDays(1).AddTicks(-1);

			var userAppointment = await _appointmentRepository.GetUnpaginatedListAsync(
				predicate: a => a.DoctorId == user.Id && a.Date >= startDate && a.Date <= endDate,
				include: a => a.Include(s => s.AppointmentStatus).Include(c => c.Client),
				enableTracking: false,
				cancellationToken: cancellationToken
			);

			var response = _mapper.Map<List<GetAllClinetAppointmentsResponse>>(userAppointment);

			return response;
		}
    }
}
