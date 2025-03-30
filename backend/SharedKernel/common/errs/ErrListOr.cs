namespace SharedKernel.common.errs;

public class ErrListOr<T>
{
    private readonly T _successValue;
    private readonly ErrList _errList;

    private ErrListOr(T value) {
        _successValue = value;
        _errList = new ErrList();
    }

    private ErrListOr(ErrList errList) {
        _errList = errList;
        _successValue = default!;
    }

    public static ErrListOr<T> Success(T value) => new(value);

    public static ErrListOr<T> Errs(ErrList errList) => new(errList);
    public bool AnyErr() => _errList.Any();

    public bool AnyErr(out ErrList errList) {
        if (_errList.Any()) {
            errList = _errList;
            return true;
        }

        errList = new ErrList();
        return false;
    }

    public bool IsSuccess() => !_errList.Any();

    public bool IsSuccess(out T success) {
        if (!AnyErr()) {
            success = _successValue;
            return true;
        }

        success = default!;
        return false;
    }

    public ErrList GetErrs() => _errList;

    public T GetSuccess() {
        if (IsSuccess()) {
            return _successValue;
        }

        throw new InvalidOperationException("No success value available.");
    }

    public static implicit operator ErrListOr<T>(ErrList errList) => new ErrListOr<T>(errList);
    public static implicit operator ErrListOr<T>(T successValue) => new ErrListOr<T>(successValue);
}