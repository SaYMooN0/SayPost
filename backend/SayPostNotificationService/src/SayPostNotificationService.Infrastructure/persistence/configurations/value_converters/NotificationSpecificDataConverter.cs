using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SayPostNotificationService.Domain.app_user_aggregate;

namespace SayPostNotificationService.Infrastructure.persistence.configurations.value_converters;

public class NotificationSpecificDataConverter : ValueConverter<BaseNotificationSpecificData, string>
{
    public NotificationSpecificDataConverter() : base(
        v => ToJson(v),
        v => FromJson(v)
    ) { }

    private static string ToJson(BaseNotificationSpecificData v) =>
        JsonSerializer.Serialize(v);

    private static BaseNotificationSpecificData FromJson(string v) =>
        JsonSerializer.Deserialize<BaseNotificationSpecificData>(v);

    
}