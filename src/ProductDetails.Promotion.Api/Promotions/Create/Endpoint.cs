using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using ProductDetails.Domain.Promotions.Commands;
using ProductDetails.Infrastructure.Auth;
namespace ProductDetails.Promotion.Api.Promotions.Insert;

public class Endpoint(IMediator mediator) : Endpoint<Request, Results<Ok, ProblemDetails>>
{
    private readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Post("/promotions");
        Policies(AuthConstants.AdminPolicy);
    }

    public override async Task HandleAsync(Request request, CancellationToken cancellationToken)
    {
        await _mediator.Send(new CreatePromotionCommand(
            request.Stockcode,
            request.PromotionalPrice,
            request.StartDateUtc,
            request.EndDateUtc), cancellationToken);

        await SendResultAsync(TypedResults.Ok());
    }
}
