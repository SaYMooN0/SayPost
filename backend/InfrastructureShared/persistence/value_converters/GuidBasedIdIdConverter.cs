using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SharedKernel.common.domain.ids;

namespace InfrastructureShared.persistence.value_converters;

public class GuidBasedIdIdConverter<TId> : ValueConverter<TId, Guid> where TId : GuidBasedId
{
    public GuidBasedIdIdConverter() : base(
        id => id.Value,
        value => (TId)Activator.CreateInstance(typeof(TId), value)
    ) { }
}