using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;

namespace SayPostMainService.Application.interfaces;

public interface ICurrentActorProvider
{
    public ErrOr<AppUserId> UserId { get; }
}