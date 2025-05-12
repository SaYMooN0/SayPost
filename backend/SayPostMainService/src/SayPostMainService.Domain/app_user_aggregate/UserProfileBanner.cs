using SayPostMainService.Domain.app_user_aggregate.profile_banner;
using SayPostMainService.Domain.common;
using SayPostMainService.Domain.rules;
using SharedKernel.common.domain.entity;
using SharedKernel.common.errs;

namespace SayPostMainService.Domain.app_user_aggregate;

public class UserProfileBanner : Entity<UserProfileBannerId>
{
    private UserProfileBanner() { }
    public float Scale { get; private set; }
    public BannerDesign Design { get; private set; }
    public BannerDesignVariant DesignVariant { get; private set; }
    public HexColor[] Colors { get; private set; }

    private UserProfileBanner(
        UserProfileBannerId id,
        float scale,
        BannerDesign design,
        BannerDesignVariant designVariant,
        HexColor[] colors
    ) {
        Scale = scale;
        Design = design;
        DesignVariant = designVariant;
        Colors = colors;
    }

    public ErrOrNothing Update(float scale, BannerDesign design, BannerDesignVariant designVariant, HexColor[] colors) {
        if (
            UserProfileBannerRules.CheckScaleValueForErr(scale).IsErr(out var err) ||
            UserProfileBannerRules.CheckColorsCountForErr(colors.Length).IsErr(out err)
        ) {
            return err;
        }

        Scale = scale;
        Design = design;
        DesignVariant = designVariant;
        Colors = colors;
        return ErrOrNothing.Nothing;
    }

    public static UserProfileBanner CreateNew() => new UserProfileBanner(
        UserProfileBannerId.CreateNew(), 1f, BannerDesign.Waves, BannerDesignVariant.Var1, [
            HexColor.FromString("#FF0000").AsSuccess(),
            HexColor.FromString("#FF3C63").AsSuccess(),
            HexColor.FromString("#FF66B2").AsSuccess(),
            HexColor.FromString("#CC33FF").AsSuccess(),
            HexColor.FromString("#9900FF").AsSuccess()
        ]);
}