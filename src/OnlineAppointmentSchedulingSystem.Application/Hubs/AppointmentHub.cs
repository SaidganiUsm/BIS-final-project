using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace OnlineAppointmentSchedulingSystem.Application.Hubs
{
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class AppointmentHub : Hub
	{
		public async Task JoinLotGroup(int doctorId)
		{
			await Groups.AddToGroupAsync(Context.ConnectionId, doctorId.ToString());
		}

		public async Task LeaveLotGroup(int doctorId)
		{
			await Groups.RemoveFromGroupAsync(Context.ConnectionId, doctorId.ToString());
		}
	}
}
