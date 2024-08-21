using FastEndpoints;

namespace ProductDetails.Api.Products.Insert;

public class Summary : Summary<Endpoint>
{
    public Summary()
    {
        Summary = "Inserts a product";
        Description = "This endpoint will insert a product to the database.";
        ExampleRequest = new Request("1-1", "Instrument Cable 3m", "Shielded instrument cable 3m", "cable", 19);
    }
}
