using System.Text;

namespace SharedKernel.common.errs;

public class Err
{
    public string Message { get; init; }
    public ushort Code { get; init; }
    public string? Details { get; init; }

    public Err(
        string message,
        ushort code = ErrCodes.Unspecified,
        string? details = null
    ) {
        Message = message;
        Code = code;
        Details = details;
    }

    public override string ToString() {
        var sb = new StringBuilder();
        if (Code != ErrCodes.Unspecified) {
            sb.AppendLine($"Code: {Code}");
        }

        sb.AppendLine($"Message: {Message}");

        if (!string.IsNullOrEmpty(Details)) {
            sb.AppendLine($"Details: {Details}");
        }

        return sb.ToString();
    }

    
    public static class ErrFactory
    {
        public static Err NotImplemented(string message = "Not Implemented", string details = "") =>
            new(message, ErrCodes.NotImplemented, details);

        public static Err NotFound(string message = "Not Found", string details = "") =>
            new(message, ErrCodes.NotFound, details);

        public static Err Unauthorized(string message = "Unauthorized Access", string details = "") =>
            new(message, ErrCodes.UnauthorizedAccess, details);

        public static Err InvalidData(string message = "Invalid Data", string details = "") =>
            new(message, ErrCodes.InvalidData, details);

        public static Err NoAccess(string message = "Access is denied", string details = "") =>
            new(message, ErrCodes.NoAccess, details);
    }

    public static class ErrCodes
    {
        public const ushort Unspecified = 0;
        public const ushort NotImplemented = 1;

        public const ushort NotFound = 1001;
        public const ushort UnauthorizedAccess = 1002;
        public const ushort InvalidData = 1003;
        public const ushort NoAccess = 1004;
    }
}