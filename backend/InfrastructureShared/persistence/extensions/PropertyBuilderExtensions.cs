using InfrastructureShared.persistence.value_converters;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.common.domain.ids;

namespace InfrastructureShared.persistence.extensions;

public static class PropertyBuilderExtensions
{
    public static PropertyBuilder<TId> HasGuidBasedIdConversion<TId>(
        this PropertyBuilder<TId> builder
    ) where TId : GuidBasedId =>
        builder.HasConversion<GuidBasedIdIdConverter<TId>>();
}