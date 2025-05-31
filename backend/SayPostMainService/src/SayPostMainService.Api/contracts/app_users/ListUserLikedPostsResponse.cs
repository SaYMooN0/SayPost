namespace SayPostMainService.Api.contracts.app_users;

public record class ListUserLikedPostsResponse(PostBriefData[] Posts);

public record PostBriefData(string PostTitle, string PostAuthor);