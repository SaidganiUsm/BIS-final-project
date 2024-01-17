namespace OnlineAppointmentSchedulingSystem.Core.Persistence.Dynamics
{
	public class DynamicQuery
	{
		public Filter? Filter { get; set; }

		public IEnumerable<Sort>? Sort { get; set; }

		public DynamicQuery() { }

		public DynamicQuery(IEnumerable<Sort>? sort, Filter? filter)
		{
			Filter = filter;
			Sort = sort;
		}
	}
}
