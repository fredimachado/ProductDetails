using FastEndpoints;
using ProductDetails.Api.Data;
using ProductDetails.Api.Data.Entities;

namespace ProductDetails.Api.Products.Upsert;

public class Endpoint(IProductRepository productRepository) : Endpoint<Request, Response>
{
    private readonly IProductRepository _productRepository = productRepository;

    public override void Configure()
    {
        Post("/products/upsert");
        Policies("AdminsOnly");
    }

    public override async Task HandleAsync(Request request, CancellationToken cancellationToken)
    {
        var product = new Product(request.Stockcode, request.Name, request.Description, request.Price);

        var (inserted, updated) = await _productRepository.UpsertAsync(product, cancellationToken);

        await SendOkAsync(new Response(inserted, updated), cancellationToken);
    }
}
