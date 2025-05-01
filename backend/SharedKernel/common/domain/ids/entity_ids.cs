namespace SharedKernel.common.domain.ids;

public class AppUserId(Guid value) : GuidBasedId(value)
{
    public static AppUserId CreateNew() => new(Guid.CreateVersion7());
}

public class PublishedPostId(Guid value) : GuidBasedId(value)
{
    public static PublishedPostId CreateNew() => new(Guid.CreateVersion7());
}
public class PostCommentId(Guid value) : GuidBasedId(value)
{
    public static PostCommentId CreateNew() => new(Guid.CreateVersion7());
}