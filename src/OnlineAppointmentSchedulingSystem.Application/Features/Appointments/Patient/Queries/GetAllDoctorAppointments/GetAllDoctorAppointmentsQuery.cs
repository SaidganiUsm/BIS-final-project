﻿using AutoMapper;
using MediatR;
using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using OnlineAppointmentSchedulingSystem.Core.Enums;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Patient.Queries.GetAllDoctorAppointments
{
	public class GetAllDoctorAppointmentsQuery : IRequest<List<GetAllDoctorAppointmentsResponse>>
	{
		public int Id { get; set; }

		public DateTime? DateTime { get; set; }
	}

	public class GetAllDoctorAppointmentsQueryHandler 
		: IRequestHandler<GetAllDoctorAppointmentsQuery, List<GetAllDoctorAppointmentsResponse>>
	{
		private readonly IAppointmentRepository _appointmentRepository;
		private readonly IMapper _mapper;

        public GetAllDoctorAppointmentsQueryHandler(
			IAppointmentRepository appointmentRepository,
			IMapper mapper
		)
		{
			_appointmentRepository = appointmentRepository;
			_mapper = mapper;
		}

        public async Task<List<GetAllDoctorAppointmentsResponse>> Handle(GetAllDoctorAppointmentsQuery request, CancellationToken cancellationToken)
		{
			DateTime queryDate = request.DateTime ?? DateTime.Today;
			var startDate = queryDate.Date; 
			var endDate = startDate.AddDays(1).AddTicks(-1);

			var approved = AppointmentStatusEnum.Approved;
			var pendingApproval = AppointmentStatusEnum.PendingApproval;
			var done = AppointmentStatusEnum.Done;


			var userAppointments = await _appointmentRepository.GetUnpaginatedListAsync(
				predicate: a => a.DoctorId == request.Id 
					&& a.Date >= startDate 
					&& a.Date <= endDate
					&& (a.AppointmentStatus!.Name == approved.ToString()
						|| a.AppointmentStatus!.Name == pendingApproval.ToString()
						|| a.AppointmentStatus!.Name == done.ToString()),
				include: a => a.Include(d => d.Doctor)
							.Include(s => s.AppointmentStatus!),
				enableTracking: false,
				cancellationToken: cancellationToken
			);

			var response = _mapper.Map<List<GetAllDoctorAppointmentsResponse>>(userAppointments);

			return response;
		}
	}
}
