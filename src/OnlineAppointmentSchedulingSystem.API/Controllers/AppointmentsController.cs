using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Commands.Create;
using OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Commands.Delete;
using OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Commands.Update;
using OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Doctor.Commands.Consider;
using OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Doctor.Queries.GetAllClientAppointments;
using OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Patient.Queries.GetAppointmentById;
using OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Patient.Queries.GetUsersAppointments;
using OnlineAppointmentSchedulingSystem.Application.Features.Staff.Commands.Create;
using OnlineAppointmentSchedulingSystem.Core.Enums;

namespace OnlineAppointmentSchedulingSystem.API.Controllers
{
    [ApiController]
	[Route("api/[controller]")]
	public class AppointmentsController : ControllerBase
	{
		private readonly IMediator _mediator;

        public AppointmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

		[HttpPost("Create")]
		[Authorize(Roles = "Patient")]
		public async Task<IActionResult> Create([FromBody] CreateAppointmentCommand command)
		{
			var result = await _mediator.Send(command);
			return Ok(result);
		}

		[HttpPut("Update")]
		[Authorize(Roles = "Patient")]
		public async Task<IActionResult> Update([FromBody] UpdateAppointmentCommand command)
		{
			var result = await _mediator.Send(command);
			return Ok(result);
		}

		[HttpDelete("Delete")]
		[Authorize(Roles = "Patient")]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			var result = await _mediator.Send(new DeleteAppointmentCommand { Id = id });

			return Ok(
				result.WasDeleted
					? $"Successfully deleted lot with id: {result.Id}"
					: $"Could not delete appointment with id: {result.Id} since its status is {AppointmentStatusEnum.Approved},"
						+ $" but successfully updated status to {AppointmentStatusEnum.Cancelled}."
			);
		}

		[HttpGet("patient-appointments")]
		[Authorize(Roles = "Patient")]
		public async Task<IActionResult> GetAllPatientAppointments()
		{
			var query = new GetUsersAppointmentsQuery();
			var appointments = await _mediator.Send(query);
			return Ok(appointments);
		}

		[HttpGet("{id}")]
		[Authorize(Roles = "Patient, Doctor, Staff")]
		public async Task<IActionResult> Get(int id)
		{
			var query = new GetAppointmentByIdQuery { Id = id };
			var appointment = await _mediator.Send(query);

			if (appointment == null)
			{
				return NotFound();
			}

			return Ok(appointment);
		}

		[HttpGet("doctor-appointments")]
		[Authorize(Roles = "Doctor")]
		public async Task<IActionResult> GetAllDoctorAppointments()
		{
			var query = new GetAllClinetAppointmentsQuery();
			var appointments = await _mediator.Send(query);
			return Ok(appointments);
		}

		[HttpPut("consider")]
		[Authorize(Roles = "Doctor")]
		public async Task<IActionResult> Consider([FromBody] ConsiderAppointmentCommand command)
		{
			var appointments = await _mediator.Send(command);
			return Ok(appointments);
		}

		[HttpPost("create/staff")]
		[Authorize(Roles = "Staff")]
		public async Task<IActionResult> CreateAppointmentStaff([FromBody] CreateAppoitnmentByStaffCommand command)
		{
			var appointment = await _mediator.Send(command);
			return Ok(appointment);
		}
	}
}
