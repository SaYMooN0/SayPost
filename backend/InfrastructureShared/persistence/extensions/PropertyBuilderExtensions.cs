using InfrastructureShared.persistence.value_converters;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.common.domain.ids;

namespace InfrastructureShared.persistence.extensions;

public static class PropertyBuilderExtensions
{
    public static PropertyBuilder<TId> HasGuidBasedIdConversion<TId>(
        this PropertyBuilder<TId> builder
    ) where TId : GuidBasedId => builder.HasConversion<GuidBasedIdConverter<TId>>();

    public static PropertyBuilder<HashSet<T>> HasGuidBasedIdsHashSetConversion<T>(
        this PropertyBuilder<HashSet<T>> builder
    ) where T : GuidBasedId {
        builder.HasConversion(
            new GuidBasedIdHashSetConverter<T>(),
            new GuidBasedIdHashSetComparer<T>()
        );
        return builder;
    }
}