using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SharedKernel.common.domain.ids;

namespace FollowingsQueryLib.configurations;

internal class AppUserIdHashSetConverter : ValueConverter<HashSet<AppUserId>, string>
{
    public AppUserIdHashSetConverter()
        : base(
            ids => string.Join(',', ids.Select(id => id.ToString())),
            str => str.Split(',', StringSplitOptions.RemoveEmptyEntries) 
                .Select(id => new AppUserId(Guid.Parse(id))) 
                .ToHashSet()) 
    { }
}
internal class AppUserIdHashSetComparer : ValueComparer<HashSet<AppUserId>> 
{
    public AppUserIdHashSetComparer() : base(
      (t1, t2) => t1!.SequenceEqual(t2!),
      t => t.Select(x => x!.GetHashCode()).Aggregate((x, y) => x ^ y),
      t => t) {
    }
}