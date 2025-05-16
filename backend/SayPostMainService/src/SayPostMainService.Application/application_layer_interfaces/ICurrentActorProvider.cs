using SharedKernel.common.domain.ids;

namespace SayPostMainService.Application.application_layer_interfaces;

public interface ICurrentActorProvider
{
    AppUserId AppUserId { get; }
}