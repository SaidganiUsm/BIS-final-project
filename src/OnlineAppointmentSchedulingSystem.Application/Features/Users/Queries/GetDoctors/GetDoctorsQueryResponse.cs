using OnlineAppointmentSchedulingSystem.Application.Common.DTOs;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Users.Queries.GetDoctors
{
	public class GetDoctorsQueryResponse
	{
		public int Id { get; set; }

		public string? FirstName { get; set; }

		public string? LastName { get; set; }

		public CategoryDto? Category { get; set; }
	}
}
