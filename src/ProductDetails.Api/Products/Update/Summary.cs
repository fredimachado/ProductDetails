using FastEndpoints;

namespace ProductDetails.Api.Products.Update;

public class Summary : Summary<Endpoint>
{
    public Summary()
    {
        Summary = "Updates a product";
        Description = "This endpoint will update a product to the database.";
        ExampleRequest = new Request("1-1", "Instrument Cable 3m", "Shielded instrument cable 3m", "cable", 19);
    }
}
