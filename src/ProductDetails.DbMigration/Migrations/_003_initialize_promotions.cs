using ProductDetails.Infrastructure.Data.Promotions;

namespace ProductDetails.DbMigration.Migrations;

public class _003_initialize_promotions : IMigration
{
    public async Task UpgradeAsync()
    {
        await DB.CreateCollectionAsync<PromotionEntity>(options => { });

        await DB.Index<PromotionEntity>()
            .Key(p => p.Stockcode, KeyType.Ascending)
            .Option(o => o.Unique = true)
            .CreateAsync();
    }
}
