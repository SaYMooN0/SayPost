using ApiShared;
using SharedKernel.common.domain.ids;
using SharedKernel.common.errs.utils;

namespace SayPostAuthService.Api.contracts.app_users;

public class GetUsernamesByIdsRequest : IRequestWithValidationNeeded
{
    public string[] Ids { get; set; } = [];
    public AppUserId[] ParsedIds => Ids.Select(i => new AppUserId(new Guid(i))).ToArray();
    const int MaxCount = 1000;

    public RequestValidationResult Validate() {
        if (Ids.Length == 0) {
            return ErrFactory.NoValue("User ids are not provided");
        }

        if (Ids.Length > MaxCount) {
            return ErrFactory.NoValue(
                $"Cannot process more than {MaxCount} user ids at once",
                $"Provided ids count: {Ids.Length}"
            );
        }

        List<string> incorrectIds = [];
        foreach (var id in Ids) {
            if (!Guid.TryParse(id, out _)) {
                incorrectIds.Add(id);
            }
        }

        if (incorrectIds.Count != 0) {
            return ErrFactory.InvalidData(
                "Some of provided ids are invalid",
                $"Invalid ids ({incorrectIds.Count}) {string.Join(", ", incorrectIds)}"
            );
        }

        return RequestValidationResult.Success;
    }
}