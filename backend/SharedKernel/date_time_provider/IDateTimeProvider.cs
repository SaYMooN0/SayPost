namespace SharedKernel.interfaces;

public interface IDateTimeProvider
{
    DateTime Now { get; }
    DateOnly NowDateOnly { get; }
}