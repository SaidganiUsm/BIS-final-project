using Microsoft.AspNetCore.Identity;

namespace OnlineAppointmentSchedulingSystem.Core.Entities
{
	public class User : IdentityUser<int>
	{
		public string? FirstName { get; set; }

		public string? LastName { get; set;}
	}
}
