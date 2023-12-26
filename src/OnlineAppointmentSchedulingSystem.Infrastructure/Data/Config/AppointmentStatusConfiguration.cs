using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineAppointmentSchedulingSystem.Core.Entities;
using System.Security.Cryptography;

namespace OnlineAppointmentSchedulingSystem.Infrastructure.Data.Config
{
	public class AppointmentStatusConfiguration : IEntityTypeConfiguration<AppointmentStatus>
	{
		public void Configure(EntityTypeBuilder<AppointmentStatus> builder)
		{
			builder.Property(n => n.Name)
				.HasMaxLength(50)
				.IsRequired(true);
		}
	}
}
