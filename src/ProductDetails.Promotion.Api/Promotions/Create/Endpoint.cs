using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using ProductDetails.Domain.Promotions.UseCases;
using ProductDetails.Infrastructure.Auth;
namespace ProductDetails.Promotion.Api.Promotions.Insert;

public class Endpoint(CreatePromotion createPromotion) : Endpoint<Request, Results<Ok, ProblemDetails>>
{
    private readonly CreatePromotion _createPromotion = createPromotion;

    public override void Configure()
    {
        Post("/promotions");
        Policies(AuthConstants.AdminPolicy);
    }

    public override async Task HandleAsync(Request request, CancellationToken cancellationToken)
    {
        await _createPromotion.ExecuteAsync(
            request.Stockcode,
            request.PromotionalPrice,
            request.StartDate,
            request.EndDate,
            cancellationToken);

        await SendResultAsync(TypedResults.Ok());
    }
}
