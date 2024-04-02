namespace OnlineAppointmentSchedulingSystem.Application.Features.Appointments.BaseValidators
{
	public interface IAppointmentCommandValidator
	{
		public DateTime Date { get; set; }

		public int DoctorId { get; set; }
	}
}
