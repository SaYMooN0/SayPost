using MediatR;
using SayPostMainService.Domain.app_user_aggregate;
using SayPostMainService.Domain.common.interfaces.repositories;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Application.app_users.commands;

public record class UpdateUserStatisticsVisibilityCommand(
    AppUserId UserId,
    StatisticsVisibilitySettings NewSettings
) : IRequest<ErrOr<StatisticsVisibilitySettings>>;

public record UserProfileStatisticsVisibilitySettings(
);

internal class UpdateUserStatisticsVisibilityCommandHandler :
    IRequestHandler<UpdateUserStatisticsVisibilityCommand, ErrOr<StatisticsVisibilitySettings>>
{
    private readonly IAppUsersRepository _appUsersRepository;

    public UpdateUserStatisticsVisibilityCommandHandler(
        IAppUsersRepository appUsersRepository
    ) {
        _appUsersRepository = appUsersRepository;
    }


    public async Task<ErrOr<StatisticsVisibilitySettings>> Handle(
        UpdateUserStatisticsVisibilityCommand command, CancellationToken cancellationToken
    ) {
        AppUser? user = await _appUsersRepository.GetById(command.UserId);
        if (user is null) {
            return ErrFactory.NotFound(
                "Cannot update user statistics visibility settings because user not found",
                $"User with id: {command.UserId} was not found"
            );
        }

        user.UpdateStatisticsVisibilitySettings(command.NewSettings);

        await _appUsersRepository.Update(user);
        return user.StatisticsVisibilitySettings;
    }
}