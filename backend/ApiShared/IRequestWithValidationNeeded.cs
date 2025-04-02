using SharedKernel.common.errs;

namespace ApiShared;

public interface IRequestWithValidationNeeded
{
    public RequestValidationResult Validate();
}

public class RequestValidationResult
{
    private ErrList _errs;

    public RequestValidationResult(ErrList errs) {
        _errs = errs;
    }

    public bool AnyErrs(out Err[] errors) {
        if (_errs.Any()) {
            errors = _errs.ToArray();
            return true;
        }

        errors = [];
        return false;
    }


    public static implicit operator RequestValidationResult(Err err) => new(err);

    public static implicit operator RequestValidationResult(ErrList errList) => new(errList);

    public static readonly RequestValidationResult Success = new(new ErrList());
}