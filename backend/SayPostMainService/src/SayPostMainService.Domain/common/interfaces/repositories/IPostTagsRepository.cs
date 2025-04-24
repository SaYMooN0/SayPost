namespace SayPostMainService.Domain.common.interfaces.repositories;

public interface IPostTagsRepository
{
    Task<string[]> TagIdValuesWithSubstring(string searchQuery, int count);
}