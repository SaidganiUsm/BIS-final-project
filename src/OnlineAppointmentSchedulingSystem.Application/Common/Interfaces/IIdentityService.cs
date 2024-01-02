using OnlineAppointmentSchedulingSystem.Application.Common.Models.Account;

namespace OnlineAppointmentSchedulingSystem.Application.Common.Interfaces
{
	public interface IIdentityService
	{
		Task<RegisterResponse> RegisterUserAsync(RegisterViewModel model);

		Task<RegisterResponse> ConfirmUserEmailAsync(string userId, string token);

		Task<ResetPasswordResponse> ForgetPasswordAsync(string email);

		Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel model);

		Task<LoginResponse> LoginUserAsync(LoginViewModel userModel);

		Task<ChangePasswordResponse> ChangeUserPasswordAsync(string email, ChangePasswordViewModel model);
	}
}
