using AutoMapper;
using MediatR;
using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Queries.GetAllClientAppointments
{
	public class GetAllClinetAppointmentsQuery : IRequest<GetAllClinetAppointmentsResponse>
	{

	}

	public class GetAllClinetAppointmentsQueryHandler : IRequestHandler<GetAllClinetAppointmentsQuery, GetAllClinetAppointmentsResponse>
	{
		private readonly ICurrentUserService _currentUserService;
		private readonly IMapper _mapper;

		public GetAllClinetAppointmentsQueryHandler(
			ICurrentUserService currentUserService,
			IMapper mapper
		)
		{
			_currentUserService = currentUserService;
			_mapper = mapper;
		}

		public Task<GetAllClinetAppointmentsResponse> Handle(
			GetAllClinetAppointmentsQuery request, 
			CancellationToken cancellationToken
		)
		{
			throw new NotImplementedException();
		}
	}
}
