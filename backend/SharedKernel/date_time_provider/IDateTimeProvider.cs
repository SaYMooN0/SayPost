namespace SharedKernel.date_time_provider;

public interface IDateTimeProvider
{
    DateTime Now { get; }
    DateOnly NowDateOnly { get; }
}