using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineAppointmentSchedulingSystem.Application.Features.Users.Commands.AssignDoctorRole;
using OnlineAppointmentSchedulingSystem.Application.Features.Users.Queries.GetDoctors;

namespace OnlineAppointmentSchedulingSystem.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class DoctorsController : ControllerBase
	{
		private readonly IMediator _mediator;

		public DoctorsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("doctors")]
		public async Task<IActionResult> GetDoctors()
		{
			var query = new GetDoctorsQuery();
			var doctors = await _mediator.Send(query);
			return Ok(doctors);
		}

		[HttpPut("doctor-role")]
		public async Task<IActionResult> AssignDoctorRole(AssignDoctorRoleCommand command)
		{
			var doctor = await _mediator.Send(command);
			return Ok(doctor);
		}
	}
}
