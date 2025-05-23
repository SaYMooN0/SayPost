using SharedKernel.common.domain.ids;

namespace SayPostFollowingsService.Application.interfaces;

public interface ICurrentActorProvider
{
    AppUserId AppUserId { get; }
}