namespace SayPostMainService.Api.contracts.app_users.statistics;

public record class ListUserCommentedPostsResponse(UserCommentedPostData[] Posts);

public record class UserCommentedPostData(string PostId, string PostAuthorId, string PostTitle, string[] Comments);