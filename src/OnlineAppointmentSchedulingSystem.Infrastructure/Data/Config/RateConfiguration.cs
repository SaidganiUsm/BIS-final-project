using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineAppointmentSchedulingSystem.Core.Entities;

namespace OnlineAppointmentSchedulingSystem.Infrastructure.Data.Config
{
	public class RateConfiguration : IEntityTypeConfiguration<Rate>
	{
		public void Configure(EntityTypeBuilder<Rate> builder)
		{
			builder.HasOne(r => r.Doctor)
				.WithMany(r => r.ReceiverRates)
				.IsRequired(true)
				.OnDelete(DeleteBehavior.NoAction);

			builder.HasOne(r => r.Client)
				.WithMany(r => r.SenderRates)
				.IsRequired(true)
				.OnDelete(DeleteBehavior.NoAction);

			builder.HasOne(r => r.Appointment)
				.WithOne(l => l.Rate)
				.HasForeignKey<Rate>(r => r.AppointmentId)
				.IsRequired(false);

			builder.Property(r => r.RatingValue).IsRequired(true);
			builder.Property(r => r.Comment).HasMaxLength(2048).IsRequired(false);
		}
	}
}
