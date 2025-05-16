using SayPostMainService.Domain.common;

namespace SayPostMainService.Application.mediatr_behavior.restrictors;

public interface IRequiresDraftPostAccessCheck
{
    DraftPostId DraftPostId { get; }
}
