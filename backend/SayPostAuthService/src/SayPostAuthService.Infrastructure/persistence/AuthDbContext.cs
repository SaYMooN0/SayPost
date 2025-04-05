using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SayPostAuthService.Domain.app_user_aggregate;
using SayPostAuthService.Domain.unconfirmed_app_user_aggregate;
using SharedKernel.common.domain;
using SharedKernel.common.domain.ids;

namespace SayPostAuthService.Infrastructure.persistence;

public class AuthDbContext : DbContext
{
    private readonly IPublisher _publisher;

    public DbSet<AppUser> AppUsers { get; init; } = null!;
    public DbSet<UnconfirmedAppUser> UnconfirmedAppUsers { get; init; } = null!;


    public AuthDbContext(DbContextOptions<AuthDbContext> options, IPublisher publisher) : base(options) {
        _publisher = publisher;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
       
        var domainEvents = ChangeTracker.Entries()
            .Where(e => e.Entity is AggregateRoot<IEntityId>)
            .Select(e => (AggregateRoot<IEntityId>)e.Entity)
            .SelectMany(ar => ar.PopAndClearDomainEvents())
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