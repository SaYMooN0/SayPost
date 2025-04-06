using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SayPostAuthService.Domain.common;

namespace SayPostAuthService.Infrastructure.persistence.configurations.value_converters;

public class EmailConverter : ValueConverter<Email, string>
{
    public EmailConverter() : base(
        id => id.ToString(),
        value => Email.Create(value).AsSuccess()
    ) { }
}