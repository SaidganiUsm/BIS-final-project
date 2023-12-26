using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces.Repositories;
using OnlineAppointmentSchedulingSystem.Core.Entities;
using OnlineAppointmentSchedulingSystem.Core.Persistence.Repositories;
using OnlineAppointmentSchedulingSystem.Infrastructure.Presistence;

namespace OnlineAppointmentSchedulingSystem.Infrastructure.Repositories
{
	public class AppointmentRepository : EfBaseRepository<Appointment, ApplicationDbContext>, IAppointmentRepository
	{
		public AppointmentRepository(ApplicationDbContext dbContext) : base(dbContext)
		{
		}
	}
}
