using InfrastructureShared.persistence.extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SayPostNotificationService.Domain.app_user_aggregate;
using SayPostNotificationService.Infrastructure.persistence.configurations.value_converters;

namespace SayPostNotificationService.Infrastructure.persistence.configurations.entities_configurations;

internal class NotificationsConfigurations : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder) {
        builder
            .HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .ValueGeneratedNever()
            .HasGuidBasedIdConversion();

        builder
            .Property(x=>x.TypeSpecificData)
            .HasConversion<NotificationSpecificDataConverter>()
            .HasColumnType("jsonb");
           
    }
}