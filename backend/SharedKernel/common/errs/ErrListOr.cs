namespace SharedKernel.common.errs;

public class ErrListOr<T>
{
    private readonly T _successValue;
    private readonly ErrList _errList;

    public ErrListOr() {
        _successValue = default;
        _errList = new ErrList();
    }

    private ErrListOr(ErrList errList) {
        _errList = errList;
        _successValue = default!;
    }

    private ErrListOr(T value) {
        _successValue = value;
        _errList = new ErrList();
    }

    public static ErrListOr<T> Success(T value) => new(value);
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

    public ErrList AsErrs() => _errList;

    public T AsSuccess() {
        if (IsSuccess()) {
            return _successValue;
        }

        throw new InvalidOperationException("No success value available.");
    }

    public TResult Match<TResult>(Func<T, TResult> successFunc, Func<ErrList, TResult> errorFunc) =>
        IsSuccess() ? successFunc(_successValue) : errorFunc(_errList);

    public static implicit operator ErrListOr<T>(ErrList errList) => new(errList);
    public static implicit operator ErrListOr<T>(T successValue) => new(successValue);
    public static implicit operator ErrListOr<T>(Err singleErr) => new(new ErrList(singleErr));
}