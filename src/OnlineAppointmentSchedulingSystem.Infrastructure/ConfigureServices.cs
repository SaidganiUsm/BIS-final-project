using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OnlineAppointmentSchedulingSystem.Infrastructure
{
	public static class ConfigureServices
	{
		public static IServiceCollection AddInfrastructureServices(
			this IServiceCollection services,
			IConfiguration configuration
		)
		{
			// Add DbContext service
			services.AddDbContext<ApplicationDbContext>(
				options =>
					options.UseSqlServer(
						configuration.GetConnectionString("DefaultConnection"),
						builder =>
							builder
								.EnableRetryOnFailure(maxRetryCount: 5)
								.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
					)
			);

			return services;
		}
	}
}
