using FastEndpoints;
using ProductDetails.Domain.Promotions;
using ProductDetails.Infrastructure.Auth;
using DomainPromotion = ProductDetails.Domain.Promotions.Promotion;

namespace ProductDetails.Promotion.Api.Promotions.Insert;

public class Endpoint(IPromotionRepository promotionRepository) : Endpoint<Request>
{
    private readonly IPromotionRepository _promotionRepository = promotionRepository;

    public override void Configure()
    {
        Post("/promotions/insert");
        Policies(AuthConstants.AdminPolicy);
    }

    public override async Task HandleAsync(Request request, CancellationToken cancellationToken)
    {
        var promotion = new DomainPromotion(request.Stockcode, request.PromotionalPrice, request.StartDate, request.EndDate);

        await _promotionRepository.InsertAsync(promotion, cancellationToken);

        await SendOkAsync(cancellationToken);
    }
}
