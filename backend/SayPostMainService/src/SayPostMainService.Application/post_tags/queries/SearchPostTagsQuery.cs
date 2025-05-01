using System.Collections.Immutable;
using MediatR;
using SayPostMainService.Domain.common;
using SayPostMainService.Domain.common.interfaces.repositories;
using SharedKernel.common.errs;
using SharedKernel.common.errs.utils;

namespace SayPostMainService.Application.post_tags.queries;

public record class SearchPostTagsQuery(string SearchValue, int Count) :
    IRequest<ErrOr<ImmutableArray<string>>>;

internal class SearchPostTagsQueryHandler
    : IRequestHandler<SearchPostTagsQuery, ErrOr<ImmutableArray<string>>>
{
    private readonly IPostTagsRepository _postTagsRepository;

    public SearchPostTagsQueryHandler(IPostTagsRepository postTagsRepository) {
        _postTagsRepository = postTagsRepository;
    }

    public async Task<ErrOr<ImmutableArray<string>>> Handle(
        SearchPostTagsQuery request, CancellationToken cancellationToken
    ) {
        ErrOrNothing validationRes = CheckSearchValueForErr(request.SearchValue);
        if (validationRes.IsErr(out var err)) {
            return err;
        }

        int count = request.Count > 20 ? 20 : request.Count;

        var tags = await _postTagsRepository.TagIdValuesWithSubstring(request.SearchValue, count);

        return ErrOr<ImmutableArray<string>>.Success([request.SearchValue, ..tags]);
    }

    private static ErrOrNothing CheckSearchValueForErr(string val) {
        if (string.IsNullOrWhiteSpace(val)) {
            return ErrFactory.InvalidData("Tag cannot be empty");
        }

        if (val.Length > PostTagId.MaxTagLength) {
            return ErrFactory.InvalidData(
                $"Tag length is too long. Maximum allowed length of tag is {PostTagId.MaxTagLength} characters "
            );
        }

        if (!PostTagId.IsStringValidTag(val)) {
            return ErrFactory.InvalidData("Provided value is not a valid post tag");
        }

        return ErrOrNothing.Nothing;
    }
}