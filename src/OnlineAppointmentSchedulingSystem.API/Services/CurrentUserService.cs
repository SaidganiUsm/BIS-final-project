using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces;
using System.Security.Claims;

namespace OnlineAppointmentSchedulingSystem.API.Services
{
	public class CurrentUserService : ICurrentUserService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public CurrentUserService(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public string? UserEmail => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);
	}
}
