using FastEndpoints;
using FluentValidation;

namespace ProductDetails.Promotion.Api.Promotions.Insert;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Stockcode)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(10);

        RuleFor(x => x.PromotionalPrice)
            .GreaterThan(0);

        RuleFor(x => x.EndDateUtc)
            .GreaterThan(DateTimeOffset.UtcNow);
    }
}
