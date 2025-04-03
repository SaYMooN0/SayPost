using MediatR;

namespace SharedKernel.common.domain;

public interface IDomainEvent : INotification;

public interface ICommand<out ResponseT> : IRequest<ResponseT>;

public interface ICommandHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>;

public interface IQuery<out ResponseT> : IRequest<ResponseT>;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>;