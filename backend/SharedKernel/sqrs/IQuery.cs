using MediatR;

namespace SharedKernel.sqrs;

public interface IQuery : IRequest;

public interface IQuery<TResponse> : IRequest<TResponse>, IQuery where TResponse : class;

public interface ICachedQuery
{
    string CacheKey { get; }
    TimeSpan? Expiration { get; }
}

public interface ICachedQuery<TResponse> : IQuery<TResponse>, ICachedQuery where TResponse : class;