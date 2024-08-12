using FastEndpoints;

namespace ProductDetails.Api.Products.Upsert;

public class Summary : Summary<Endpoint>
{
    public Summary()
    {
        Summary = "Inserts or updates a product";
        Description = "This endpoint will insert a product to the database if it's new or update an existing product based on stockcode.";
        ExampleRequest = new Request("1-1", "Intel Laptop", "16\" Laptop with Intel i5, 16Gb RAM and 1TB SSD", 1900);
    }
}
