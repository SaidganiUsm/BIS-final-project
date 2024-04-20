using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineAppointmentSchedulingSystem.Core.Entities;

namespace OnlineAppointmentSchedulingSystem.Infrastructure.Data.Config
{
	public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
	{
		public void Configure(EntityTypeBuilder<Appointment> builder)
		{
			builder.HasOne(a => a.Doctor)
				.WithMany(u => u.DoctorAppointments)
				.HasForeignKey(a => a.DoctorId)
				.IsRequired(true)
				.OnDelete(DeleteBehavior.NoAction);

			builder.HasOne(a => a.Client)
				.WithMany(u => u.ClientAppointments)
				.HasForeignKey(a => a.ClientId)
				.IsRequired(true)
				.OnDelete(DeleteBehavior.NoAction);

			builder.HasOne(l => l.AppointmentStatus)
				.WithMany(ls => ls.Appointments)
				.HasForeignKey(l => l.AppointmentStatusId)
				.IsRequired(true);

			builder.Property(r => r.Result).HasMaxLength(1000).IsRequired(false);

			builder.Property(d => d.Date).IsRequired(true);
		}
	}
}
