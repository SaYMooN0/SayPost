using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Domain.rules;

public static class UserProfileBannerRules
{
    const int
        ScaleMinValue = 1,
        ScaleMaxValue = 2;

    private const int ColorsCount = 5;

    public static ErrOrNothing CheckScaleValueForErr(float scaleValue) {
        if (scaleValue < ScaleMinValue) {
            return ErrFactory.InvalidData(
                $"Banner scale value too small. Minimum allowed is {ScaleMinValue}, but got {scaleValue}");
        }

        if (scaleValue > ScaleMaxValue) {
            return ErrFactory.InvalidData(
                $"Banner scale value too large. Maximum allowed is {ScaleMaxValue}, but got {scaleValue}");
        }

        return ErrOrNothing.Nothing;
    }


    public static ErrOrNothing CheckColorsCountForErr(int colorsCount) =>
        colorsCount == ColorsCount
            ? ErrOrNothing.Nothing
            : ErrFactory.InvalidData($"Incorrect banner colors count. Expected: {ColorsCount}, Actual: {colorsCount}");
}