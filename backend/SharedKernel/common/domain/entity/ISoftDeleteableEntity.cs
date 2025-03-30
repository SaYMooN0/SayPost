namespace SharedKernel.common.domain.entity;

public interface ISoftDeleteableEntity
{
    public bool IsDeleted { get; }
    public DateTime? DeletedAt { get; }
}