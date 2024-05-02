using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using OnlineAppointmentSchedulingSystem.Application.Common.DTOs;
using OnlineAppointmentSchedulingSystem.Application.Common.Interfaces.Repositories;
using OnlineAppointmentSchedulingSystem.Core.Entities;
using OnlineAppointmentSchedulingSystem.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.Threading;

namespace OnlineAppointmentSchedulingSystem.Application.Features.Users.Queries.GetDoctors
{
	public class GetDoctorsQuery : IRequest<List<GetDoctorsQueryResponse>>
	{

	}

	public class GetDoctorQueryHandler : IRequestHandler<GetDoctorsQuery, List<GetDoctorsQueryResponse>>
	{
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<Role> _roleManager;
		private readonly IMapper _mapper;
		private readonly ICategoryRepository _categoryRepository;

        public GetDoctorQueryHandler(
			UserManager<User> userManager,
			RoleManager<Role> roleManager,
			IMapper mapper,
			ICategoryRepository categoryRepository
		)
        {
			_roleManager = roleManager;
			_userManager = userManager;
			_mapper = mapper;
			_categoryRepository = categoryRepository;
        }

		public async Task<List<GetDoctorsQueryResponse>> Handle(GetDoctorsQuery request, CancellationToken cancellationToken)
		{
			UserRolesEnum role = UserRolesEnum.Doctor;

			var doctorRole = await _roleManager.FindByNameAsync(role.ToString());

			if (doctorRole == null)
			{
				throw new ValidationException("The role 'Doctor' does not exist.");
			}

			var doctors = await _userManager.GetUsersInRoleAsync(doctorRole.Name);

			var result = new List<GetDoctorsQueryResponse>();

			foreach (var doctor in doctors)
			{
				var category = await _categoryRepository.GetAsync(c => c.Id == doctor.CategoryId);

				var doctorResponse = _mapper.Map<GetDoctorsQueryResponse>(doctor);
				doctorResponse.Category = _mapper.Map<CategoryDto>(category);

				result.Add(doctorResponse);
			}

			return result;
		}
	}
}
