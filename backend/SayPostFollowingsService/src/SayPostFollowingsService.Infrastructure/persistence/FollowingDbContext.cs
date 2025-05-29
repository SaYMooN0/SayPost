using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SayPostFollowingsService.Domain.app_user_aggregate;
using SharedKernel.common.domain;

namespace SayPostFollowingsService.Infrastructure.persistence;

public class FollowingDbContext : DbContext
{
    private readonly IPublisher _publisher;

    public DbSet<AppUser> AppUsers { get; init; } = null!;


    public FollowingDbContext(DbContextOptions<FollowingDbContext> options, IPublisher publisher) : base(options) {
        _publisher = publisher;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
        var domainEvents = ChangeTracker.Entries()
            .Where(e => e.Entity is IAggregateRoot)
            .SelectMany(ar => (ar.Entity as IAggregateRoot).PopAndClearDomainEvents())
            .ToList();

        await PublishDomainEvents(domainEvents);
        return await base.SaveChangesAsync(cancellationToken);
    }

    private async Task PublishDomainEvents(List<IDomainEvent> domainEvents) {
        foreach (var domainEvent in domainEvents) {
            await _publisher.Publish(domainEvent);
        }
    }
}