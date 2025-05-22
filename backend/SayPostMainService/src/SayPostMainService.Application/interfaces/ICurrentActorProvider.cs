using SharedKernel.common.domain.ids;

namespace SayPostMainService.Application.interfaces;

public interface ICurrentActorProvider
{
    AppUserId AppUserId { get; }
}