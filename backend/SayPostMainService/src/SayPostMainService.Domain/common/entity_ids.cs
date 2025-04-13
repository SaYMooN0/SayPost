using SharedKernel.common.domain.ids;

namespace SayPostMainService.Domain.common;

public class DraftPostId(Guid value) : GuidBasedId(value)
{
    public static DraftPostId CreateNew() => new(Guid.CreateVersion7());
}
