using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ProductDetails.Promotion.Api.Promotions.Insert;

public class Summary : Summary<Endpoint>
{
    public Summary()
    {
        Summary = "Inserts a promotion";
        Description = "This endpoint will insert a promotion to the database.";
        ExampleRequest = new Request("1-1", 1500, DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddDays(1));
    }
}
