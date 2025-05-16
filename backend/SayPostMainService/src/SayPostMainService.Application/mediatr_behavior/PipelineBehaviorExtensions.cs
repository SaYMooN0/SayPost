namespace SayPostMainService.Application.mediatr_behavior;

using SharedKernel.common.errs;
using System.Reflection;

public static class PipelineBehaviorExtensions
{
    public static TResponse CreateErrResponse<TResponse>(Err error) {
        var type = typeof(TResponse);

        // Handle ErrOrNothing
        if (type == typeof(ErrOrNothing)) {
            return (TResponse)(object)ErrOrNothingFromErr(error);
        }

        // Handle ErrOr<T>
        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ErrOr<>)) {
            Type innerType = type.GetGenericArguments()[0];

            ConstructorInfo? ctor = typeof(ErrOr<>)
                .MakeGenericType(innerType)
                .GetConstructor([typeof(Err)]);

            if (ctor is not null) {
                return (TResponse)ctor.Invoke([error]);
            }
        }

        // Handle ErrListOr<T>
        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ErrListOr<>)) {
            Type innerType = type.GetGenericArguments()[0];

            ConstructorInfo? ctor = typeof(ErrListOr<>)
                .MakeGenericType(innerType)
                .GetConstructor([typeof(ErrList)]);

            if (ctor is not null) {
                return (TResponse)ctor.Invoke([new ErrList(error)]);
            }
        }


        throw new InvalidOperationException($"Cannot create Err response for type {type.FullName}");
    }

    private static ErrOrNothing ErrOrNothingFromErr(Err err) => err;
}