using ProductDetails.Infrastructure.Data.Promotions;

namespace ProductDetails.DbMigration.Migrations;

public class _004_delete_promotions_stockcode_index : IMigration
{
    public async Task UpgradeAsync()
    {
        await DB.Index<PromotionEntity>()
            .DropAsync("Stockcode(Asc)");
    }
}
