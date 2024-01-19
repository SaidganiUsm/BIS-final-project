using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineAppointmentSchedulingSystem.Core.Entities;
using OnlineAppointmentSchedulingSystem.Infrastructure.Common;
using OnlineAppointmentSchedulingSystem.Infrastructure.Interceptors;
using System.Reflection;

namespace OnlineAppointmentSchedulingSystem.Infrastructure.Presistence
{
	public class ApplicationDbContext : IdentityDbContext<User, Role, int>
	{
		private readonly IMediator? _mediator;
		private readonly AuditableEntitySaveChangesInterceptor? _auditableEntitiesInterceptor;

		public ApplicationDbContext() { }

		public ApplicationDbContext(DbContextOptions options,
			AuditableEntitySaveChangesInterceptor auditableEntitiesInterceptor,
			IMediator mediator) : base(options)
		{
			_auditableEntitiesInterceptor = auditableEntitiesInterceptor;
			_mediator = mediator;
		}

		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}

		public virtual DbSet<Appointment> Appointments { get; set; }

		public virtual DbSet<Rate> Rates { get; set; }

		public virtual DbSet<Role> Roles { get; set; }

		public virtual DbSet<Category> Categories { get; set; }

		public virtual DbSet<AppointmentStatus> AppointmentsStatuses { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<User>()
				.Ignore(u => u.AccessFailedCount)
				.Ignore(u => u.LockoutEnabled)
				.Ignore(u => u.LockoutEnd)
				.Ignore(u => u.TwoFactorEnabled)
				.Ignore(u => u.PhoneNumberConfirmed);

			builder.Entity<User>().ToTable("Users");
			builder.Entity<Role>().ToTable("Roles");
			builder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");
			builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
			builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
			builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
			builder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");

			builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.AddInterceptors(_auditableEntitiesInterceptor);
		}

		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			await _mediator.DispatchDomainEvents(this);

			return await base.SaveChangesAsync(cancellationToken);
		}
	}
}
