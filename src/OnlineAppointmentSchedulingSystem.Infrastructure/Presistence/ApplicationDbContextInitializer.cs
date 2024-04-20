using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OnlineAppointmentSchedulingSystem.Core.Entities;
using OnlineAppointmentSchedulingSystem.Infrastructure.Common.Options;
using Quartz.Logging;
using System.Security.Policy;

namespace OnlineAppointmentSchedulingSystem.Infrastructure.Presistence
{
	public class ApplicationDbContextInitializer
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<Role> _roleManager;
		private readonly UsersSeedingData _usersData;

		public ApplicationDbContextInitializer(
			ApplicationDbContext context,
			UserManager<User> userManager,
			RoleManager<Role> roleManager,
			IOptions<UsersSeedingData> usersData
		)
		{
			_context = context;
			_userManager = userManager;
			_roleManager = roleManager;
			_usersData = usersData.Value;
		}

		public async Task InitialiseAsync()
		{
			try
			{
				if (_context.Database.IsSqlServer())
				{
					await _context.Database.MigrateAsync();
				}
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		public async Task SeedAsync()
		{
			try
			{
				await TrySeedAsync();
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		public async Task TrySeedAsync()
		{
			// Default roles
			var roles = new List<Role>
			{
				new Role { Name = "Administrator", },
				new Role { Name = "Doctor", },
				new Role { Name = "Patient", },
				new Role { Name = "Staff", }
			};

			foreach (var role in roles)
			{
				if (!await _roleManager.RoleExistsAsync(role.Name!))
				{
					await _roleManager.CreateAsync(role);
				}
			}

			// Default users
			var users = new List<User>
			{
				new User
				{
					UserName = _usersData.Emails.Admin,
					Email = _usersData.Emails.Admin,
					EmailConfirmed = true
				},
				new User
				{
					UserName = _usersData.Emails.Doctor,
					Email = _usersData.Emails.Doctor,
					EmailConfirmed = true
				},
				new User
				{
					UserName = _usersData.Emails.Patient,
					Email = _usersData.Emails.Patient,
					EmailConfirmed = true
				},
				new User
				{
					UserName = _usersData.Emails.Staff,
					Email = _usersData.Emails.Staff,
					EmailConfirmed = true
				},
			};

			foreach (var user in users)
			{
				if ((await _userManager.FindByNameAsync(user.UserName!) is null))
				{
					await _userManager.CreateAsync(user, "Test123!");
					switch (user.UserName)
					{
						case "admin@localhost.com":
							var adminRole = roles.Find(r => r.Name == "Administrator");
							if (adminRole != null)
							{
								await _userManager.AddToRolesAsync(
									user,
									new List<string> { adminRole.Name! }
								);
							}
							break;
						case "doctor@localhost.com":
							var doctorRole = roles.Find(r => r.Name == "Doctor");
							if (doctorRole != null)
							{
								await _userManager.AddToRolesAsync(
									user,
									new List<string> { doctorRole.Name! }
								);
							}
							break;
						case "patient@localhost.com":
							var patientRole = roles.Find(r => r.Name == "Patient");
							if (patientRole != null)
							{
								await _userManager.AddToRolesAsync(
									user,
									new List<string> { patientRole.Name! }
								);
							}
							break;
						case "staff@localhost.com":
							var staffRole = roles.Find(r => r.Name == "Staff");
							if (staffRole != null)
							{
								await _userManager.AddToRolesAsync(
									user,
									new List<string> { staffRole.Name! }
								);
							}
							break;
					}
				}
			}

			if (!_context.AppointmentsStatuses.Any())
			{
				_context.AppointmentsStatuses.AddRange(
					new AppointmentStatus { Name = "PendingApproval"},
					new AppointmentStatus { Name = "Rejected"},
					new AppointmentStatus { Name = "Approved"},
					new AppointmentStatus { Name = "Done"},
					new AppointmentStatus { Name = "Cancelled"}
				);

				await _context.SaveChangesAsync();
			}

			if (!_context.Categories.Any())
			{
				var electronicsCategory = _context.Categories.Add(
					new Category { CategoryName = "Analysis" }
				);

				var furnitureCategory = _context.Categories.Add(
					new Category { CategoryName = "Test" }
				);

				await _context.SaveChangesAsync();
			}
		}
	}
}
