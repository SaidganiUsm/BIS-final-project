using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces.Repositories;
using OnlineAppointmentSchedulingSystem.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Users.Queries.GetById
{
	public class GetByIdUserQuery : IRequest<GetByIdUserResponse>
	{
		public string Id { get; set; }
	}

	public class GetByIdQueryHandler : IRequestHandler<GetByIdUserQuery, GetByIdUserResponse>
	{
		private readonly UserManager<User> _userManager;
		private readonly ICategoryRepository _categoryRepository;
		private readonly IMapper _mapper;

        public GetByIdQueryHandler(
			UserManager<User> userManager,
			ICategoryRepository categoryRepository,
			IMapper mapper
		)
        {
            _categoryRepository = categoryRepository;
			_userManager = userManager;
			_mapper = mapper;
        }
        public async Task<GetByIdUserResponse> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByIdAsync(request.Id);

			if (user != null)
			{
				if (user.CategoryId != null)
				{
					var category = await _categoryRepository.GetAsync(
					predicate: c => c.Id == user.CategoryId,
					cancellationToken: cancellationToken
					);

					user.Category = category;
				}

				var result = _mapper.Map<GetByIdUserResponse>(user);

				return result;
			}

			return new GetByIdUserResponse();
		}
	}
}
