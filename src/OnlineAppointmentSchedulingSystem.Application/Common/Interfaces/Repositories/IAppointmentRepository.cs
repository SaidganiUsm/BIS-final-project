using OnlineAppointmentSchedulingSystem.Core.Entities;
using OnlineAppointmentSchedulingSystem.Core.Persistence.Repositories;

namespace OnlineAppointmentSchedulingSystem.Application.Common.Interfaces.Repositories
{
	public interface IAppointmentRepository : IAsyncRepository<Appointment>
	{

	}
}
