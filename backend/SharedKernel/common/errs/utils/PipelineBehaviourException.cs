namespace SharedKernel.common.errs.utils;

public class PipelineBehaviourException : Exception
{
    public Err[] Errs { get; }
    private PipelineBehaviourException() { }
    public PipelineBehaviourException(Err err) => Errs = [err];
    public PipelineBehaviourException(Err[] errs) => Errs = errs;
}
