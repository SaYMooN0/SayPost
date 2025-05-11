using System.Text.Json.Serialization;
using SayPostNotificationService.Domain.app_user_aggregate.notification_type_specific_data;
using SharedKernel.common.domain;

namespace SayPostNotificationService.Domain.app_user_aggregate;

[JsonDerivedType(typeof(TestPublishedNotificationSpecificData),
    typeDiscriminator: nameof(TestPublishedNotificationSpecificData))]
[JsonDerivedType(typeof(CommentLeftNotificationSpecificData),
    typeDiscriminator: nameof(CommentLeftNotificationSpecificData))]
public abstract class BaseNotificationSpecificData : ValueObject
{
    public abstract Dictionary<string, string> ToDictionary();
}