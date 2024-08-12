
namespace ProductDetails.Api.Products;

public class ProductService : IProductService
{
    public Task<IEnumerable<ProductModel>> GetProductsAsync(string[] stockcodes)
    {
        return Task.FromResult<IEnumerable<ProductModel>>(new[]
        {
            new ProductModel
            {
                Stockcode = "12345",
                Name = "Product 1",
                Description = "Description of Product 1",
                Price = 10.00m
            },
            new ProductModel
            {
                Stockcode = "67890",
                Name = "Product 2",
                Description = "Description of Product 2",
                Price = 20.00m
            }
        });
    }
}
