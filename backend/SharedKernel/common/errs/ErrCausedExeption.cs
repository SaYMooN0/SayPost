namespace SharedKernel.common.errs;

public class ErrCausedException : Exception
{
    public Err Err { get; set; }
    private ErrCausedException() { }
    public ErrCausedException(Err err) { Err = err; }
}
