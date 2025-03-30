namespace SharedKernel.common.errs;

public class ErrListOrNothing
{
    private ErrList _errList;

    private ErrListOrNothing(ErrList errs) {
        _errList = errs;
    }

    public bool IsErr() => _errList.Any();

    public bool IsErr(out ErrList errs) {
        if (_errList.Any()) {
            errs = _errList;
            return true;
        }

        errs = new();
        return false;
    }

    public static implicit operator ErrListOrNothing(ErrList errs) => new(errs);
    public static implicit operator ErrListOrNothing(Err singleErr) => new(new ErrList(singleErr));

    public static readonly ErrListOrNothing Nothing = new(new ErrList());
}