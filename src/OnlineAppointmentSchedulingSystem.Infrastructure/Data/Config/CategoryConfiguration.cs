using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineAppointmentSchedulingSystem.Core.Entities;

namespace OnlineAppointmentSchedulingSystem.Infrastructure.Data.Config
{
	public class CategoryConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.HasMany(r => r.Doctors)
				.WithOne(r => r.Category)
				.IsRequired(false)
				.OnDelete(DeleteBehavior.NoAction);

			builder.Property(r => r.CategoryName)
				.HasMaxLength(2048)
				.IsRequired(true);
		}
	}
}
