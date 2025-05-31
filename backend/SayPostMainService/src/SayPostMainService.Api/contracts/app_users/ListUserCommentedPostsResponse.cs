namespace SayPostMainService.Api.contracts.app_users;

public record class ListUserCommentedPostsResponse(UserCommentedPostData[] Comments);

public record class UserCommentedPostData(string PostId, string PostAuthorId, string PostTitle, string[] Comments);