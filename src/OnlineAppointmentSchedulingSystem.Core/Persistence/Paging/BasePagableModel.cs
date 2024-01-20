namespace OnlineAppointmentSchedulingSystem.Core.Persistence.Paging
{
	public abstract class BasePagableModel
	{
		public int Index { get; set; }

		public int Size { get; set; }

		public int Count { get; set; }

		public int Pages { get; set; }

		public bool HasPrevious { get; set; }

		public bool HasNext { get; set; }
	}
}
