namespace SharedKernel.common.errs;
public class ErrWithExtraData : Err
{
    public ErrWithExtraData(
        string message,
        Dictionary<string, object> extraData,
        ushort code = ErrCodes.Unspecified,
        string? details = null
    ) : base(message, code, details) {
        ExtraData = extraData;
    }

    public Dictionary<string, object> ExtraData { get; }
}
