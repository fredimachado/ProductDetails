using ProductDetails.Infrastructure.Data.Products;

namespace ProductDetails.DbMigration.Migrations;

public class _001_initialize_productdetails : IMigration
{
    public async Task UpgradeAsync()
    {
        await DB.CreateCollectionAsync<ProductEntity>(options => { });

        await DB.Index<ProductEntity>()
            .Key(p => p.Stockcode, KeyType.Ascending)
            .Option(o => o.Unique = true)
            .CreateAsync();
    }
}
