using OnlineAppointmentSchedulingSystem.Core.Common;

namespace OnlineAppointmentSchedulingSystem.Core.Entities
{
	public class Category : BaseAuditableEntity
	{
		public string CategoryName { get; set; }

		public virtual ICollection<User> Doctors { get; set; }
	}
}
