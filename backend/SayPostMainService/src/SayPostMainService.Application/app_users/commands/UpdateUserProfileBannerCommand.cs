using MediatR;
using SayPostMainService.Application.interfaces;
using SayPostMainService.Domain.app_user_aggregate;
using SayPostMainService.Domain.app_user_aggregate.profile_banner;
using SayPostMainService.Domain.common.interfaces.repositories;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Application.app_users.commands;

public record class UpdateUserProfileBannerCommand(
    float Scale,
    BannerDesign Design,
    BannerDesignVariant Variant,
    HexColor[] Colors
) : IRequest<ErrOr<UserProfileBanner>>;

internal class UpdateUserProfileBannerCommandHandler :
    IRequestHandler<UpdateUserProfileBannerCommand, ErrOr<UserProfileBanner>>
{
    private readonly IAppUsersRepository _appUsersRepository;
private readonly ICurrentActorProvider _currentActorProvider;
    public UpdateUserProfileBannerCommandHandler(
        IAppUsersRepository appUsersRepository, ICurrentActorProvider currentActorProvider
        ) {
        _appUsersRepository = appUsersRepository;
        _currentActorProvider = currentActorProvider;
    }


    public async Task<ErrOr<UserProfileBanner>> Handle(
        UpdateUserProfileBannerCommand command, CancellationToken cancellationToken
    ) {
        var userId = _currentActorProvider.UserId.AsSuccess();
        
        AppUser? user = await _appUsersRepository.GetWithBanner(userId);
        if (user is null) {
            return ErrFactory.NotFound("Unknown user", $"User with id: {userId} was not found");
        }

        var result = user.UpdateProfileBanner(command.Scale, command.Design, command.Variant, command.Colors);
        if (result.IsErr(out var err)) {
            return err;
        }
        await _appUsersRepository.Update(user);
        return user.ProfileBanner;
    }
}