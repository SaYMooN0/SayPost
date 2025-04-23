using System.Text.Json.Serialization;
using MediatR;

namespace SharedKernel.integration_events;

[JsonDerivedType(typeof(NewAppUserCreatedIntegrationEvent),
    typeDiscriminator: nameof(NewAppUserCreatedIntegrationEvent))]

[JsonDerivedType(typeof(NewPostPublishedIntegrationEvent),
    typeDiscriminator: nameof(NewPostPublishedIntegrationEvent))]
public interface IIntegrationEvent : INotification { }