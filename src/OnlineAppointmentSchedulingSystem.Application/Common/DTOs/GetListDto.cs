namespace OnlineAppointmentSchedulingSystem.Application.Common.DTOs
{
	public class GetListDto<T> : BasePageableModel
	{
		public IList<T> Items
		{
			get => _items ??= new List<T>();
			set => _items = value;
		}

		private IList<T>? _items;
	}
}
