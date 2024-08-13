using MongoDB.Entities;
using ProductDetails.Domain.Products;

namespace ProductDetails.Infrastructure.Data.Products;

internal class ProductRepository : IProductRepository
{
    public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await DB.Find<ProductEntity, Product>()
            .Project(p => new(p.Stockcode, p.Name, p.Description, p.Price))
            .ExecuteAsync(cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetByStockcodeAsync(string[] stockcodes, CancellationToken cancellationToken)
    {
        return await DB.Find<ProductEntity, Product>()
            .Match(p => stockcodes.Contains(p.Stockcode))
            .Project(p => new(p.Stockcode, p.Name, p.Description, p.Price))
            .ExecuteAsync(cancellationToken);
    }

    public async Task<Product?> GetByStockcodeAsync(string stockcode, CancellationToken cancellationToken)
    {
        return await DB.Find<ProductEntity, Product>()
            .Match(p => p.Stockcode == stockcode)
            .Project(p => new(p.Stockcode, p.Name, p.Description, p.Price))
            .ExecuteFirstAsync(cancellationToken);
    }

    public async Task InsertAsync(Product product, CancellationToken cancellationToken)
    {
        var entity = new ProductEntity(product.Stockcode, product.Name, product.Description, product.Price);

        await DB.InsertAsync(entity, cancellation: cancellationToken);
    }

    public async Task UpdateAsync(Product product, CancellationToken cancellationToken)
    {
        var entity = new ProductEntity(product.Stockcode, product.Name, product.Description, product.Price);

        await DB.Update<ProductEntity>()
            .Match(p => p.Stockcode == product.Stockcode)
            .ModifyWith(entity)
            .ExecuteAsync(cancellationToken);
    }
}
