using OnlineAppointmentSchedulingSystem.Application.Common.DTOs;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Users.Queries.GetUserProfile
{
	public class GetUserProfileResponse
	{
		public int Id { get; set; }

		public string? FirstName { get; set; }

		public string? LastName { get; set; }

		public string? AboutMe { get; set; }

		public string? PhoneNumber { get; set; }

		public int? Expirience { get; set; }

		public CategoryDto Category { get; set; }
	}
}
