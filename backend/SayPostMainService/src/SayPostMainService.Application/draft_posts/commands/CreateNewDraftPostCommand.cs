using MediatR;
using SayPostMainService.Domain.common.interfaces.repositories;
using SayPostMainService.Domain.draft_post_aggregate;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;
using SharedKernel.date_time_provider;

namespace SayPostMainService.Application.draft_posts.commands;

public record class CreateNewDraftPostCommand(AppUserId UserId) :
    IRequest<ErrOr<DraftPost>>;

internal class CreateNewDraftPostCommandHandler
    : IRequestHandler<CreateNewDraftPostCommand, ErrOr<DraftPost>>
{
    private readonly IAppUsersRepository _appUsersRepository;
    private readonly IDraftPostsRepository _draftPostsRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreateNewDraftPostCommandHandler(
        IAppUsersRepository appUsersRepository,
        IDraftPostsRepository draftPostsRepository,
        IDateTimeProvider dateTimeProvider
    ) {
        _appUsersRepository = appUsersRepository;
        _draftPostsRepository = draftPostsRepository;
        _dateTimeProvider = dateTimeProvider;
    }


    public async Task<ErrOr<DraftPost>> Handle(
        CreateNewDraftPostCommand command, CancellationToken cancellationToken
    ) {
        bool userExists = await _appUsersRepository.DoesUserExist(command.UserId);
        if (!userExists) {
            return ErrFactory.NotFound(
                "User not found",
                $"User with id {command.UserId} that is trying to create a new post was not found"
            );
        }

        DraftPost post = DraftPost.CreateNew(command.UserId, _dateTimeProvider);
        await _draftPostsRepository.Add(post);

        return post;
    }
}