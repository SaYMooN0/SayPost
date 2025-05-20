using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SayPostMainService.Domain.app_user_aggregate;
using SayPostMainService.Domain.draft_post_aggregate;
using SayPostMainService.Domain.post_comment_aggregate;
using SayPostMainService.Domain.post_tag_aggregate;
using SayPostMainService.Domain.published_post_aggregate;
using SharedKernel.common.domain;
using SharedKernel.common.domain.ids;

namespace SayPostMainService.Infrastructure.persistence;

public class MainDbContext : DbContext
{
    private readonly IPublisher _publisher;

    public DbSet<AppUser> AppUsers { get; init; } = null!;
    public DbSet<DraftPost> DraftPosts { get; init; } = null!;
    public DbSet<PublishedPost> PublishedPosts { get; init; } = null!;
    public DbSet<PostTag> PostTags { get; init; } = null!;
    public DbSet<PostComment> PostComments { get; init; } = null!;


    public MainDbContext(DbContextOptions<MainDbContext> options, IPublisher publisher) : base(options) {
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