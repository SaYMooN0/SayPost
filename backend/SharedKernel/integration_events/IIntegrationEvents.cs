using System.Text.Json.Serialization;
using MediatR;

namespace SharedKernel.integration_events;

[JsonDerivedType(typeof(NewAppUserCreatedIntegrationEvent),
    typeDiscriminator: nameof(NewAppUserCreatedIntegrationEvent))]

[JsonDerivedType(typeof(NewPostPublishedIntegrationEvent),
    typeDiscriminator: nameof(NewPostPublishedIntegrationEvent))]

[JsonDerivedType(typeof(NewCommentUnderPostLeftIntegrationEvent),
    typeDiscriminator: nameof(NewCommentUnderPostLeftIntegrationEvent))]
public interface IIntegrationEvent : INotification { }