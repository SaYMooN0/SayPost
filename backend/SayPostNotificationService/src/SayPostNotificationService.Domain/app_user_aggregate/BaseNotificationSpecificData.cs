using System.Text.Json.Serialization;
using SayPostNotificationService.Domain.app_user_aggregate.notification_type_specific_data;
using SharedKernel.common.domain;

namespace SayPostNotificationService.Domain.app_user_aggregate;

[JsonDerivedType(typeof(PostPublishedNotificationSpecificData),
    typeDiscriminator: nameof(PostPublishedNotificationSpecificData))]
[JsonDerivedType(typeof(CommentLeftNotificationSpecificData),
    typeDiscriminator: nameof(CommentLeftNotificationSpecificData))]
[JsonDerivedType(typeof(PostLikedNotificationSpecificData),
    typeDiscriminator: nameof(PostLikedNotificationSpecificData))]
[JsonDerivedType(typeof(UserGotFollowerNotificationSpecificData),
    typeDiscriminator: nameof(UserGotFollowerNotificationSpecificData))]
public abstract class BaseNotificationSpecificData : ValueObject
{
    public abstract NotificationType Type();
    public abstract Dictionary<string, string> ToDictionary();
}