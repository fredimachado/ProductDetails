
namespace ProductDetails.Api.Products;

public interface IProductService
{
    Task<IEnumerable<ProductModel>> GetProductsAsync(string[] stockcodes);
}
