using MediatR;
using Microsoft.AspNetCore.Identity;
using OnlineAppointmentSchedulingSystem.Core.Entities;
using OnlineAppointmentSchedulingSystem.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Users.Commands.AssignDoctorRole
{
	public class AssignDoctorRoleCommand : IRequest<AssignDoctorRoleCommandResponse>
	{
		public string Email { get; set; }
	}

	public class AssignDoctorRoleCommandHandler 
		: IRequestHandler<AssignDoctorRoleCommand, AssignDoctorRoleCommandResponse>
	{
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<Role> _roleManager;

        public AssignDoctorRoleCommandHandler(
			UserManager<User> userManager,
			RoleManager<Role> roleManager
		)
        {
            _roleManager = roleManager;
			_userManager = userManager;
        }
        public async Task<AssignDoctorRoleCommandResponse> Handle(AssignDoctorRoleCommand request, CancellationToken cancellationToken)
		{
			var role = UserRolesEnum.Doctor;
			var user = await _userManager.FindByEmailAsync(request.Email);
			if (user == null)
			{
				return null;
			}

			var isInRole = await _userManager.IsInRoleAsync(user, role.ToString());
			if (isInRole)
			{
				throw new ValidationException("User has same role");
			}

			var doctorRole = await _roleManager.FindByNameAsync(role.ToString());
			if (doctorRole == null)
			{
				return null;
			}

			var existingRoles = await _userManager.GetRolesAsync(user);
			await _userManager.RemoveFromRolesAsync(user, existingRoles);

			var result = await _userManager.AddToRoleAsync(user, role.ToString());
			if (!result.Succeeded)
			{
				return null;
			}

			return new AssignDoctorRoleCommandResponse { Id = user.Id };
		}
	}
}
