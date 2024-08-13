using FastEndpoints;
using FluentValidation;

namespace ProductDetails.Api.Products.Insert;

public class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Stockcode)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(10);

        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(50);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(2000);

        RuleFor(x => x.Price)
            .GreaterThan(0);
    }
}
