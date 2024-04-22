using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces.Repositories;
using OnlineAppointmentSchedulingSystem.Core.Entities;
using OnlineAppointmentSchedulingSystem.Core.Persistence.Repositories;
using OnlineAppointmentSchedulingSystem.Infrastructure.Presistence;

namespace OnlineAppointmentSchedulingSystem.Infrastructure.Repositories
{
	public class CategoryRepository : EfBaseRepository<Category, ApplicationDbContext>, ICategoryRepository
	{
		public CategoryRepository(ApplicationDbContext context) : base(context)
		{
		}
	}
}
