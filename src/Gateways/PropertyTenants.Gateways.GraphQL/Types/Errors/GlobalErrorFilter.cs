using HotChocolate;

namespace PropertyTenants.Gateways.GraphQL.Types.Errors;

public class GlobalErrorFilter : IErrorFilter
{
    public IError OnError(IError error)
    {
        return error.Exception switch
        {
            ArgumentException => error.WithMessage("Invalid argument provided"),
            UnauthorizedAccessException => error.WithMessage("Access denied"),
            KeyNotFoundException => error.WithMessage("Resource not found"),
            InvalidOperationException => error.WithMessage("Invalid operation"),
            _ => error
        };
    }
}
