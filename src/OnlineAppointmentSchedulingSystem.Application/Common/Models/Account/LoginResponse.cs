using OnlineAppointmentSchedulingSystem.Application.Common.Models.BaseModels;

namespace OnlineAppointmentSchedulingSystem.Application.Common.Models.Account
{
	public class LoginResponse : BaseResponse
	{
		public TokenModel Result { get; set; }
	}
}
