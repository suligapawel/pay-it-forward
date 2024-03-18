using Microsoft.AspNetCore.Http;

namespace PayItForward.Shared.Requests;

public class RequestValidatorFilter : IEndpointFilter
{
    public async ValueTask<object> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var request = context.Arguments.OfType<IRequest>().FirstOrDefault();
        if (!request?.IsValid() ?? false)
        {
            return Results.BadRequest(request.Error);
        }

        return await next(context);
    }
}