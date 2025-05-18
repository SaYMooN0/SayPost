using ApiShared;
using SayPostMainService.Domain.app_user_aggregate.profile_banner;
using SayPostMainService.Domain.rules;

namespace SayPostMainService.Api.contracts.app_users;

public class UpdateProfileBannerRequest : IRequestWithValidationNeeded
{
    private float Scale { get; init; }
    private BannerDesign Design { get; init; }
    private BannerDesignVariant Variant { get; init; }
    private string[] Colors { get; init; } = [];

    public RequestValidationResult Validate() {
        if (
            UserProfileBannerRules.CheckScaleValueForErr(Scale).IsErr(out var err) ||
            UserProfileBannerRules.CheckColorsCountForErr(Colors.Length).IsErr(out err)
        ) {
            return err;
        }

        return RequestValidationResult.Success;
    }
}