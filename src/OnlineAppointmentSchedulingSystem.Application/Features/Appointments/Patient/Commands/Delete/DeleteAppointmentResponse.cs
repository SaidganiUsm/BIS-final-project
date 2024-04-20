namespace OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Commands.Delete
{
	public class DeleteAppointmentResponse
	{
		public int Id { get; set; }

		public bool WasDeleted { get; set; } = false;
	}
}
