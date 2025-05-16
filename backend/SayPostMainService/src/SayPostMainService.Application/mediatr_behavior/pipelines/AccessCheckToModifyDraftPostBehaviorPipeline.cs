using MediatR;
using SayPostMainService.Application.application_layer_interfaces;
using SayPostMainService.Application.mediatr_behavior.restrictors;
using SayPostMainService.Domain.common.interfaces.repositories;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Application.mediatr_behavior.pipelines;

public class AccessCheckToModifyDraftPostBehaviorPipeline<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, IRequiresDraftPostAccessCheck
    where TResponse : notnull
{
    private readonly ICurrentActorProvider _currentActorProvider;
    private readonly IDraftPostsRepository _draftPostsRepository;

    public AccessCheckToModifyDraftPostBehaviorPipeline(
        ICurrentActorProvider currentActorProvider,
        IDraftPostsRepository draftPostsRepository
    ) {
        _currentActorProvider = currentActorProvider;
        _draftPostsRepository = draftPostsRepository;
    }

    public async Task<TResponse> Handle(
        TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken
    ) {
        var draftPostId = request.DraftPostId;
        var currentUserId = _currentActorProvider.AppUserId;

        var postAuthorGetRes = await _draftPostsRepository.GetPostAuthor(draftPostId);

        if (postAuthorGetRes.IsErr(out var err)) {
            return PipelineBehaviorExtensions.CreateErrResponse<TResponse>(err);
        }

        var postAuthorId = postAuthorGetRes.AsSuccess();

        if (postAuthorId != currentUserId) {
            var noAccessErr = ErrFactory.NoAccess(
                "You cannot modify this draft post",
                "You need to be the author of the post"
            );
            return PipelineBehaviorExtensions.CreateErrResponse<TResponse>(noAccessErr);
        }

        return await next();
    }
}