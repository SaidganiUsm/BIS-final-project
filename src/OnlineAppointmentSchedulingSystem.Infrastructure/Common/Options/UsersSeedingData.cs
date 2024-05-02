namespace OnlineAppointmentSchedulingSystem.Infrastructure.Common.Options
{
	public class UsersSeedingData
	{
		public UserEmails Emails { get; set; }

		public string Password { get; set; }
	}

	public class UserEmails
	{
		public string Admin { get; set; }

		public string Doctor { get; set; }

		public string Patient { get; set; }

		public string Staff { get; set; }
	}
}
