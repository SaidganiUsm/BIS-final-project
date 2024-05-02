using Microsoft.AspNetCore.Mvc;
using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces;
using OnlineAppointmentSchedulingSystem.Application.Common.Models.Account;

namespace OnlineAppointmentSchedulingSystem.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : Controller
	{
		private readonly IIdentityService _identityService;
		private readonly ICurrentUserService _currentUserService;

		public AuthController(
			IIdentityService identityService,
			ICurrentUserService currentUserService
		)
		{
			_identityService = identityService;
			_currentUserService = currentUserService;
		}

		[HttpPost]
		[Route("register")]
		public async Task<IActionResult> RegisterAsync([FromBody] RegisterViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest("Some properties are not valid.");
			}

			var result = await _identityService.RegisterUserAsync(model);

			if (!result.IsSuccess)
				return BadRequest(result);

			return Ok(result);
		}

		[HttpGet]
		[Route("confirm-email")]
		public async Task<IActionResult> ConfirmEmailAsync(string userId, string token)
		{
			if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
				return NotFound();

			var result = await _identityService.ConfirmUserEmailAsync(userId, token);

			if (!result.IsSuccess)
				return BadRequest(result);

			return Ok("Confirmed");
		}

		[HttpPost("forget-password")]
		public async Task<IActionResult> ForgetPassword(string email)
		{
			if (string.IsNullOrEmpty(email))
				return NotFound();

			var result = await _identityService.ForgetPasswordAsync(email);

			if (!result.IsSuccess)
				return BadRequest(result);

			return Ok(result);
		}

		[HttpPost("reset-password")]
		public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest("Some properties are not valid.");
			}

			var result = await _identityService.ResetPasswordAsync(model);

			if (!result.IsSuccess)
				return BadRequest(result);

			return Ok(result);
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginViewModel loginModel)
		{
			var result = await _identityService.LoginUserAsync(loginModel);

			if (!result.IsSuccess)
			{
				return BadRequest(result);
			}

			return Ok(result);
		}

		[HttpPut("change-password")]
		public async Task<IActionResult> ChangePassword([FromForm] ChangePasswordViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest("Some properties are not valid.");
			}

			var email = _currentUserService.UserEmail;

			var result = await _identityService.ChangeUserPasswordAsync(email!, model);

			if (!result.IsSuccess)
			{
				return BadRequest(result);
			}

			return Ok();
		}
	}
}
