using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SayPostMainService.Domain.common;
using SharedKernel.common.domain.ids;

namespace SayPostMainService.Infrastructure.persistence.configurations.value_converters;

internal class PostTagIdHashSetConverter : ValueConverter<HashSet<PostTagId>, string>
{
    public PostTagIdHashSetConverter()
        : base(
            ids => string.Join(',', ids.Select(id => id.ToString())),
            str => str.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(id => PostTagId.Create(id).AsSuccess())
                .ToHashSet()) { }
}

internal class PostTagIdHashSetComparer : ValueComparer<HashSet<PostTagId>>
{
    public PostTagIdHashSetComparer() : base(
        (t1, t2) => t1!.SequenceEqual(t2!),
        t => t.Select(x => x!.GetHashCode()).Aggregate((x, y) => x ^ y),
        t => t) { }
}