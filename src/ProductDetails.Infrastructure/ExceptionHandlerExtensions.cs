using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProductDetails.Domain.Exceptions;
using System.ComponentModel;
using System.Net;

namespace ProductDetails.Infrastructure;

class ExceptionHandler;

public static class ExceptionHandlerExtensions
{
    public static IApplicationBuilder UseExceptionHandler(this IApplicationBuilder app,
                                                          ILogger? logger = null,
                                                          bool useGenericReason = false)
    {
        app.UseExceptionHandler(
            errApp =>
            {
                errApp.Run(
                    async ctx =>
                    {
                        var exHandlerFeature = ctx.Features.Get<IExceptionHandlerFeature>();

                        if (exHandlerFeature is not null)
                        {
                            logger ??= ctx.RequestServices.GetRequiredService<ILogger<ExceptionHandler>>();
                            var route = exHandlerFeature.Endpoint?.DisplayName?.Split(" => ")[0];
                            var exceptionType = exHandlerFeature.Error.GetType().Name;
                            var isDomainException = exHandlerFeature.Error is DomainException;
                            var code = isDomainException ? 400 : ctx.Response.StatusCode;
                            var reason = exHandlerFeature.Error.Message;

                            logger.LogError(exHandlerFeature.Error, "[{@exceptionType}] at [{@route}] due to [{@reason}]", exceptionType, route, reason);

                            var errorResponse = new InternalErrorResponse
                            {
                                Status = isDomainException ? "Bad Request" : "Internal Server Error!",
                                Code = code,
                                Reason = useGenericReason ? "An unexpected error has occurred." : reason,
                                Note = "See application log for stack trace."
                            };

                            ctx.Response.StatusCode = code;
                            ctx.Response.ContentType = "application/problem+json";
                            await ctx.Response.WriteAsJsonAsync(errorResponse);
                        }
                    });
            });

        return app;
    }
}

public sealed record InternalErrorResponse
{
    public required string Status { get; init; }
    public required int Code { get; init; }
    public required string Reason { get; init; }
    public required string Note { get; init; }
}
