using SayPostAuthService.Domain.app_user_aggregate.events;
using SayPostAuthService.Domain.common;
using SayPostAuthService.Domain.common.interfaces;
using SharedKernel.common.domain;
using SharedKernel.common.domain.ids;

namespace SayPostAuthService.Domain.app_user_aggregate;

public class AppUser : AggregateRoot<AppUserId>
{
    private AppUser() { }
    public Email Email { get; }
    public string PasswordHash { get; }
    public string Username { get; private set; }

    private AppUser(AppUserId id, Email email, string passwordHash, string username) {
        Id = id;
        Email = email;
        PasswordHash = passwordHash;
    }

    public static AppUser CreateNew(Email email, string password, IPasswordHasher passwordHasher) {
        AppUser user = new(AppUserId.CreateNew(), email, passwordHasher.HashPassword(password), "SayPostUser");
        user._domainEvents.Add(new NewAppUserCreatedEvent(user.Id));
        return user;
    }

    public bool IsPasswordCorrect(IPasswordHasher passwordHasher, string passwordToCheck) {
        return passwordHasher.VerifyPassword(PasswordHash, passwordToCheck);
    }
}