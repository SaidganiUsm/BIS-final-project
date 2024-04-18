using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineAppointmentSchedulingSystem.Application.Common.Behaviours;
using OnlineAppointmentSchedulingSystem.Application.Common.Options;
using System.Reflection;

namespace OnlineAppointmentSchedulingSystem.Application
{
	public static class ConfigureServices
	{
		public static IServiceCollection AddApplicationServices(
			this IServiceCollection services,
			IConfiguration configuration
		)
		{
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

			services.AddAutoMapper(Assembly.GetExecutingAssembly());
			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
			services.AddMediatR(config =>
			{
				config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
			});

			services.Configure<AppUrlOptions>(configuration.GetSection(AppUrlOptions.App));

			ValidatorOptions.Global.LanguageManager.Culture = new System.Globalization.CultureInfo(
				"en-US"
			);

			return services;
		}
	}
}
