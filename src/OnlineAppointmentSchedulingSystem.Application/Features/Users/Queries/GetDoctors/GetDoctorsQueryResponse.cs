using OnlineAppointmentSchedulingSystem.Application.Common.DTOs;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Users.Queries.GetDoctors
{
	public class GetDoctorsQueryResponse
	{
		public string? FirstName { get; set; }

		public string? LastName { get; set; }

		public int? CategoryId { get; set; }

		public virtual CategoryDto? Category { get; set; }

	}
}
