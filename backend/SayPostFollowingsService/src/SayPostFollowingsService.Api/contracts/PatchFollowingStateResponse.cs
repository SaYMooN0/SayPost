namespace SayPostFollowingsService.Api.contracts;

public record PatchFollowingStateResponse(bool NewIsFollowedByViewer, int NewFollowersCount);