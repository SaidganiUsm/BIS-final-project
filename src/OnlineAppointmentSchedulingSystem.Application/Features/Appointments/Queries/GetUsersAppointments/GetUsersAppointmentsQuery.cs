using MediatR;
using OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Queries.GetUserAppointments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Queries.GetUsersAppointments
{
	public class GetUsersAppointmentsQuery : IRequest<GetUsersAppointmentsResponse>
	{
	}

	public class GetUsersAppointmentsQueryHandler
		: IRequestHandler<GetUsersAppointmentsQuery, GetUsersAppointmentsResponse>()
	{
		public Task<GetUsersAppointmentsResponse> Handle(GetUsersAppointmentsQuery request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
