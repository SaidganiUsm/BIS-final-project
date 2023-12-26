using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces;
using OnlineAppointmentSchedulingSystem.Application.Common.Models.Account;

namespace OnlineAppointmentSchedulingSystem.Infrastructure.Identity
{
	public class IdentityService : IIdentityService
	{
		public Task<LoginResponse> AssignRoleToUserAsync(string role)
		{
			throw new NotImplementedException();
		}

		public Task<ChangePasswordResponse> ChangeUserPasswordAsync(string email, ChangePasswordViewModel model)
		{
			throw new NotImplementedException();
		}

		public Task<RegisterResponse> ConfirmUserEmailAsync(string userId, string token)
		{
			throw new NotImplementedException();
		}

		public Task<ResetPasswordResponse> ForgetPasswordAsync(string email)
		{
			throw new NotImplementedException();
		}

		public Task<LoginResponse> LoginUserAsync(LoginViewModel userModel)
		{
			throw new NotImplementedException();
		}

		public Task<RegisterResponse> RegisterUserAsync(RegisterViewModel model)
		{
			throw new NotImplementedException();
		}

		public Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel model)
		{
			throw new NotImplementedException();
		}
	}
}
