using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces;
using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces.Repositories;
using OnlineAppointmentSchedulingSystem.Core.Entities;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Users.Queries.GetUserProfile
{
	public class GetUserProfileQuery : IRequest<GetUserProfileResponse>
	{

	}

	public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, GetUserProfileResponse>
	{
		private readonly UserManager<User> _userManager;
		private readonly ICurrentUserService _currentUserService;
		private readonly ICategoryRepository _categoryRepository;
		private readonly IMapper _mapper;

        public GetUserProfileQueryHandler(
			UserManager<User> userManager,
			ICurrentUserService currentUserService,
			ICategoryRepository categoryRepository,
			IMapper mapper
		)
        {
            _currentUserService = currentUserService;
			_userManager = userManager;
			_categoryRepository = categoryRepository;
			_mapper = mapper;
        }
        public async Task<GetUserProfileResponse> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByEmailAsync(_currentUserService.UserEmail!);

			var category = await _categoryRepository.GetAsync(
				predicate: c => c.Id == user.CategoryId,
				cancellationToken: cancellationToken
			);

			user.Category = category;

			var result = _mapper.Map<GetUserProfileResponse>(user);

			return result;
		}
	}
}
