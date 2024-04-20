namespace OnlineAppointmentSchedulingSystem.Application.Features.Appointments.Commands.Create
{
	public class CreateAppointmentResponse
	{
		public int Id { get; set; }

		public DateTime Date { get; set; }

		public int DoctorId { get; set; }
	}
}
