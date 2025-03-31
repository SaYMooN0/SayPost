namespace SharedKernel.common.errs;

public static class ErrExtensions
{
    public static bool IsWithExtraData(this Err err, out Dictionary<string, object> extraData) {
        if (err is ErrWithExtraData withExtraData) {
            extraData = withExtraData.ExtraData;
            return true;
        }

        extraData = [];
        return false;
    }

    public static Err ToNewWithPrefix(this Err err, string prefix) =>
        err is ErrWithExtraData errWithExtraData
            ? new ErrWithExtraData($"{prefix}: {err.Message}", errWithExtraData.ExtraData, err.Code, err.Details)
            : new Err($"{prefix}: {err.Message}", err.Code, err.Details);
}