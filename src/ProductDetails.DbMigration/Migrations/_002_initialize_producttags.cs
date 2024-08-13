using ProductDetails.Infrastructure.Data.Tags;

namespace ProductDetails.DbMigration.Migrations;

public class _002_initialize_producttags : IMigration
{
    public async Task UpgradeAsync()
    {
        await DB.CreateCollectionAsync<ProductTagEntity>(options => { });

        await DB.Index<ProductTagEntity>()
            .Key(p => p.Stockcode, KeyType.Ascending)
            .Option(o => o.Unique = true)
            .CreateAsync();
    }
}
