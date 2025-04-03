using SharedKernel.common.domain.ids;

namespace SayPostAuthService.Domain.common;

public class UnconfirmedAppUserId(Guid value) : GuidBasedId(value)
{
    public static UnconfirmedAppUserId CreateNew() => new(Guid.CreateVersion7());
}
