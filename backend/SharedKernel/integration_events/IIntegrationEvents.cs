using System.Text.Json.Serialization;
using MediatR;

namespace SharedKernel.integration_events;

[JsonDerivedType(typeof(NewAppUserCreatedIntegrationEvent), typeDiscriminator: nameof(NewAppUserCreatedIntegrationEvent))]
public interface IIntegrationEvent : INotification { }
