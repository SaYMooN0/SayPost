using System.Reflection;
using FollowingsQueryLib.entities;
using Microsoft.EntityFrameworkCore;

namespace FollowingsQueryLib;

public class UserRelationsDbContext : DbContext
{
    internal DbSet<AppUserWithFollowingsData> AppUsers { get; set; } = null!;
    public UserRelationsDbContext(DbContextOptions<UserRelationsDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
    //     ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    //     base.OnConfiguring(optionsBuilder);
    // }

    public override int SaveChanges() =>
        throw new InvalidOperationException("This context is read-only.");

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        throw new InvalidOperationException("This context is read-only.");
}