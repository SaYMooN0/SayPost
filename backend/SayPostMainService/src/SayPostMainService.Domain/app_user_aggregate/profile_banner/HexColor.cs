using SharedKernel.common.domain;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Domain.app_user_aggregate.profile_banner;

public class HexColor : ValueObject
{
    private HexColor(string hexColor) {
        _value = hexColor;
    }

    private readonly string _value;

    public static ErrOr<HexColor> FromString(string colorValue) {
        colorValue = colorValue.Trim();

        if (!IsValidHexColor(colorValue)) {
            return ErrFactory.IncorrectFormat($"Invalid hex color format: {colorValue}");
        }

        if (!colorValue.StartsWith("#")) {
            colorValue = "#" + colorValue;
        }

        return new HexColor(colorValue);
    }

    public static bool IsValidHexColor(string colorValue) {
        if (colorValue.StartsWith("#")) {
            colorValue = colorValue.Substring(1);
        }

        return System.Text.RegularExpressions.Regex.IsMatch(colorValue, "^([a-fA-F0-9]{6}|[a-fA-F0-9]{3})$");
    }

    public override IEnumerable<object> GetEqualityComponents() => [_value];

    public override string ToString() => _value;
}