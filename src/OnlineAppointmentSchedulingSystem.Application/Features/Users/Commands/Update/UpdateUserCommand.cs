using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces;
using OnlineAppointmentSchedulingSystem.Core.Entities;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Users.Commands.Update
{
	public class UpdateUserCommand : IRequest<UpdatedUserResponse>
	{
		public string? FirstName { get; set; }

		public string? LastName { get; set; }

		public string? PhoneNumber { get; set; }

		public string? AboutMe { get; set; }

		public int? Expirience { get; set; }
	}

	public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdatedUserResponse>
	{
		private readonly ICurrentUserService _currentUserService;
		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;

		public UpdateUserCommandHandler(
			ICurrentUserService currentUserService,
			UserManager<User> userManager,
			IMapper mapper
		)
		{
			_currentUserService = currentUserService;
			_userManager = userManager;
			_mapper = mapper;
		}

		async Task<UpdatedUserResponse> IRequestHandler<UpdateUserCommand, UpdatedUserResponse>.Handle(
			UpdateUserCommand request,
			CancellationToken cancellationToken
		)
		{
			var user = await _userManager.FindByEmailAsync(_currentUserService.UserEmail!);

			user.FirstName = request.FirstName;
			user.LastName = request.LastName;
			user.PhoneNumber = request.PhoneNumber;
			user.AboutMe = request.AboutMe;

			await _userManager.UpdateAsync(user);

			var response = _mapper.Map<UpdatedUserResponse>(user);

			return response;
		}
	}
}
