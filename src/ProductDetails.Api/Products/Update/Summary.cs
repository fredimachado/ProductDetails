using FastEndpoints;

namespace ProductDetails.Api.Products.Update;

public class Summary : Summary<Endpoint>
{
    public Summary()
    {
        Summary = "Updates a product";
        Description = "This endpoint will update a product to the database.";
        ExampleRequest = new Request("1-1", "Intel Laptop", "Laptop with Intel i5, 16Gb RAM and 1TB SSD", 1900);
    }
}
