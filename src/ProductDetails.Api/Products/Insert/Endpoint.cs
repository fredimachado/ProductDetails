using FastEndpoints;
using ProductDetails.Domain.Products;

namespace ProductDetails.Api.Products.Insert;

public class Endpoint(IProductRepository productRepository) : Endpoint<Request>
{
    private readonly IProductRepository _productRepository = productRepository;

    public override void Configure()
    {
        Post("/products/insert");
        Policies(AuthConstants.AdminPolicy);
    }

    public override async Task HandleAsync(Request request, CancellationToken cancellationToken)
    {
        var product = new Product(request.Stockcode, request.Name, request.Description, request.Price);

        await _productRepository.InsertAsync(product, cancellationToken);

        await SendOkAsync(cancellationToken);
    }
}
