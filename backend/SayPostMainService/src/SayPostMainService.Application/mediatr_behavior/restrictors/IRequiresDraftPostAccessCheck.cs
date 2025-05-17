using MediatR;
using SayPostMainService.Domain.common;

namespace SayPostMainService.Application.mediatr_behavior.restrictors;

public interface IRequiresDraftPostAccessCheck : IBaseRequest
{
    DraftPostId DraftPostId { get; }
}
