using MediatR;
using SayPostFollowingsService.Application.interfaces;
using SayPostFollowingsService.Domain.app_user_aggregate;
using SayPostFollowingsService.Domain.common.interfaces.repositories;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostFollowingsService.Application.app_users.commands;

public record class FollowUserCommand(AppUserId UserId) : IRequest<ErrOr<bool>>;

internal class FollowUserCommandHandler :
    IRequestHandler<FollowUserCommand, ErrOr<bool>>
{
    private readonly IAppUsersRepository _appUsersRepository;
    private readonly ICurrentActorProvider _currentActorProvider;

    public FollowUserCommandHandler(
        IAppUsersRepository appUsersRepository, ICurrentActorProvider currentActorProvider
    ) {
        _appUsersRepository = appUsersRepository;
        _currentActorProvider = currentActorProvider;
    }


    public async Task<ErrOr<bool>> Handle(FollowUserCommand command, CancellationToken cancellationToken) {
        AppUser? user = await _appUsersRepository.GetById(command.UserId);
        if (user is null) {
            return ErrFactory.NotFound("Unknown user", $"User with id: {command.UserId} was not found");
        }

        var actor = _currentActorProvider.AppUserId;
        var changeMade = user.AddFollower(actor);
        if (changeMade) {
            await _appUsersRepository.Update(user);
        }

        return user.IsFollowedBy(actor);
    }
}