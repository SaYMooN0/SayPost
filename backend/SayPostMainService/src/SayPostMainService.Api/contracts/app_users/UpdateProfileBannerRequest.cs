using ApiShared;
using SayPostMainService.Domain.app_user_aggregate.profile_banner;
using SayPostMainService.Domain.rules;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Api.contracts.app_users;

public class UpdateProfileBannerRequest : IRequestWithValidationNeeded
{
    public float Scale { get; init; }
    public BannerDesign Design { get; init; }
    public BannerDesignVariant DesignVariant { get; init; }
    public string[] Colors { get; init; } = [];

    public RequestValidationResult Validate() {
        string[] incorrectColors = Colors.Where(c => !HexColor.IsValidHexColor(c)).ToArray();
        if (incorrectColors.Any()) {
            return ErrFactory.IncorrectFormat(
                "Invalid banner colors",
                $"Invalid hex colors: {string.Join(',', incorrectColors)}"
            );
        }

        if (
            UserProfileBannerRules.CheckScaleValueForErr(Scale).IsErr(out var err) ||
            UserProfileBannerRules.CheckColorsCountForErr(Colors.Length).IsErr(out err)
        ) {
            return err;
        }

        return RequestValidationResult.Success;
    }

    public HexColor[] ParsedColors => Colors.Select(c => HexColor.FromString(c).AsSuccess()).ToArray();
}