using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces.Repositories;
using OnlineAppointmentSchedulingSystem.Core.Entities;
using OnlineAppointmentSchedulingSystem.Core.Persistence.Repositories;
using OnlineAppointmentSchedulingSystem.Infrastructure.Presistence;

namespace OnlineAppointmentSchedulingSystem.Infrastructure.Repositories
{
	public class AppointmentStatusRepository : EfBaseRepository<AppointmentStatus, ApplicationDbContext>, IAppointmentStatusRepository
	{
		public AppointmentStatusRepository(ApplicationDbContext context) : base(context)
		{
		}
	}
}
