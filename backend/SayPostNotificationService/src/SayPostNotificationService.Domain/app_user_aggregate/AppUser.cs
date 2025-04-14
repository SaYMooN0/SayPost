using SharedKernel.common.domain;
using SharedKernel.common.domain.ids;

namespace SayPostNotificationService.Domain.app_user_aggregate;

public class AppUser : AggregateRoot<AppUserId>
{
    private AppUser() { }

    public AppUser(AppUserId id) {
        Id = id;
    }
}