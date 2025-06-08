using MediatR;
using SayPostMainService.Application.interfaces;
using SayPostMainService.Domain.app_user_aggregate;
using SayPostMainService.Domain.common.interfaces.repositories;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Application.app_users.commands;

public record class UpdateUserStatisticsVisibilityCommand(
    StatisticsVisibilitySettings NewSettings
) : IRequest<ErrOr<StatisticsVisibilitySettings>>;

internal class UpdateUserStatisticsVisibilityCommandHandler :
    IRequestHandler<UpdateUserStatisticsVisibilityCommand, ErrOr<StatisticsVisibilitySettings>>
{
    private readonly IAppUsersRepository _appUsersRepository;
    private readonly ICurrentActorProvider _currentActorProvider;

    public UpdateUserStatisticsVisibilityCommandHandler(
        IAppUsersRepository appUsersRepository,
        ICurrentActorProvider currentActorProvider
    ) {
        _appUsersRepository = appUsersRepository;
        _currentActorProvider = currentActorProvider;
    }


    public async Task<ErrOr<StatisticsVisibilitySettings>> Handle(
        UpdateUserStatisticsVisibilityCommand command, CancellationToken cancellationToken
    ) {
        var userId = _currentActorProvider.UserId.AsSuccess();
        AppUser? user = await _appUsersRepository.GetById(userId);
        if (user is null) {
            return ErrFactory.NotFound(
                "Cannot update user statistics visibility settings because user not found",
                $"User with id: {userId} was not found"
            );
        }

        user.UpdateStatisticsVisibilitySettings(command.NewSettings);

        await _appUsersRepository.Update(user);
        return user.StatisticsVisibilitySettings;
    }
}