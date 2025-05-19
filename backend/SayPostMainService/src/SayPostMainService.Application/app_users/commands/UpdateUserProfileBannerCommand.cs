using MediatR;
using SayPostMainService.Domain.app_user_aggregate;
using SayPostMainService.Domain.app_user_aggregate.profile_banner;
using SayPostMainService.Domain.common.interfaces.repositories;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Application.app_users.commands;

public record class UpdateUserProfileBannerCommand(
    AppUserId UserId,
    float Scale,
    BannerDesign Design,
    BannerDesignVariant Variant,
    HexColor[] Colors
) : IRequest<ErrOr<UserProfileBanner>>;

internal class UpdateUserProfileBannerCommandHandler :
    IRequestHandler<UpdateUserProfileBannerCommand, ErrOr<UserProfileBanner>>
{
    private readonly IAppUsersRepository _appUsersRepository;

    public UpdateUserProfileBannerCommandHandler(
        IAppUsersRepository appUsersRepository
    ) {
        _appUsersRepository = appUsersRepository;
    }


    public async Task<ErrOr<UserProfileBanner>> Handle(
        UpdateUserProfileBannerCommand command, CancellationToken cancellationToken
    ) {
        AppUser? user = await _appUsersRepository.GetWithBanner(command.UserId);
        if (user is null) {
            return ErrFactory.NotFound("Unknown user", $"User with id: {command.UserId} was not found");
        }

        var result = user.UpdateProfileBanner(command.Scale, command.Design, command.Variant, command.Colors);
        if (result.IsErr(out var err)) {
            return err;
        }
        await _appUsersRepository.Update(user);
        return user.ProfileBanner;
    }
}