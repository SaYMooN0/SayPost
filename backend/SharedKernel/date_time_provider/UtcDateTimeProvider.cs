
using SharedKernel.interfaces;

namespace SharedKernel.date_time_provider;

public class UtcDateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.UtcNow;
    public DateOnly NowDateOnly => DateOnly.FromDateTime(DateTime.UtcNow);
}
