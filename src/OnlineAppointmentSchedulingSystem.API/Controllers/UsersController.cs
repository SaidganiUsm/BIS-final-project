using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineAppointmentSchedulingSystem.Application.Features.Users.Commands.Update;
using OnlineAppointmentSchedulingSystem.Application.Features.Users.Queries.GetById;
using OnlineAppointmentSchedulingSystem.Application.Features.Users.Queries.GetUserProfile;

namespace OnlineAppointmentSchedulingSystem.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UsersController : ControllerBase
	{
		private readonly IMediator _mediator;

		public UsersController(IMediator mediator) 
		{
			_mediator = mediator;
		}

		[HttpGet("user-profile")]
		public async Task<IActionResult> GetUserProfile() 
		{
			var query = new GetUserProfileQuery();
			var user = await _mediator.Send(query);
			return Ok(user);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateUserProfile(
			[FromForm] UpdateUserCommand updateUserCommand
		)
		{
			var result = await _mediator.Send(updateUserCommand);
			return Ok(result);
		}

		[HttpGet("{id}")]
		[Authorize(Roles = "Doctor, Patient, Staff")]
		public async Task<IActionResult> GetById(string id)
		{
			var result = await _mediator.Send(new GetByIdUserQuery { Id = id });

			return Ok(result);
		}
	}
}
