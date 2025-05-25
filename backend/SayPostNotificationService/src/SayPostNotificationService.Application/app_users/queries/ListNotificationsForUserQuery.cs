using MediatR;
using SayPostNotificationService.Domain.app_user_aggregate;
using SayPostNotificationService.Domain.common.interfaces.repositories;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostNotificationService.Application.app_users.queries;

public record class ListNotificationsForUserQuery(
    AppUserId UserId,
    bool ShowOnlyNotViewed
) : IRequest<ErrOr<Notification[]>>;

internal class
    ListNotificationsForUserQueryHandler : IRequestHandler<ListNotificationsForUserQuery, ErrOr<Notification[]>>
{
    private readonly IAppUsersRepository _appUsersRepository;

    public ListNotificationsForUserQueryHandler(IAppUsersRepository appUsersRepository) {
        _appUsersRepository = appUsersRepository;
    }


    public async Task<ErrOr<Notification[]>> Handle(
        ListNotificationsForUserQuery request,
        CancellationToken cancellationToken
    ) {
        AppUser? user = await _appUsersRepository.GetByIdWithNotifications(request.UserId);
        if (user is null) {
            return ErrFactory.NotFound(
                "User not found",
                $"Cannot find user with id: {request.UserId}"
            );
        }

        if (request.ShowOnlyNotViewed) {
            return user.Notifications
                .Where(n => !n.Viewed)
                .OrderByDescending(n => n.CreatedAt)
                .ToArray();
        }

        return user.Notifications
            .OrderByDescending(n => n.CreatedAt)
            .ToArray();
    }
}