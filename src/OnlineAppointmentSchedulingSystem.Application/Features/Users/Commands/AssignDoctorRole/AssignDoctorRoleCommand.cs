﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using OnlineAppointmentSchedulingSystem.Core.Entities;
using OnlineAppointmentSchedulingSystem.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Users.Commands.AssignDoctorRole
{
	public class AssignDoctorRoleCommand : IRequest<AssignDoctorRoleResponse>
	{
		public string Id { get; set; }
	}

	public class AssignDoctorRoleCommandHandler 
		: IRequestHandler<AssignDoctorRoleCommand, AssignDoctorRoleResponse>
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
        public async Task<AssignDoctorRoleResponse> Handle(AssignDoctorRoleCommand request, CancellationToken cancellationToken)
		{
			var role = UserRolesEnum.Doctor;
			var user = await _userManager.FindByIdAsync(request.Id);
			if (user == null)
			{
				throw new ValidationException("User not found!");
			}

			var isInRole = await _userManager.IsInRoleAsync(user, role.ToString());
			if (isInRole)
			{
				throw new ValidationException("User has same role!");
			}

			var doctorRole = await _roleManager.FindByNameAsync(role.ToString());
			if (doctorRole == null)
			{
				throw new ValidationException("Role does not found!");
			}

			var existingRoles = await _userManager.GetRolesAsync(user);
			await _userManager.RemoveFromRolesAsync(user, existingRoles);

			var result = await _userManager.AddToRoleAsync(user, role.ToString());
			if (!result.Succeeded)
			{
				return null;
			}

			return new AssignDoctorRoleResponse { Id = user.Id };
		}
	}
}
