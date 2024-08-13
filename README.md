# Product Details

Experiments with GraphQL, FastEndpoints and RulesEngine.

## ProductDetails.Api

This project is a GraphQL API that exposes endpoints for querying product details using
using the [HotChocolate](https://hotchocolate.io/) library.

## Fetching Product Details:

Product details can be fetched by stockcode using the following GraphQL query:

```graphql
query {
  product(stockcode: "1-1") {
    description
    name
    price
    stockcode
  }
}
```

This will result in the following response:

```json
{
  "data": {
    "product": {
      "description": "Laptop with Intel i5, 16Gb RAM and 1TB SSD",
      "name": "Intel Laptop",
      "price": 1900,
      "stockcode": "1-1"
    }
  }
}
```

## Product Tags

The Product model was extended to include tags. Tags can be added to a product using the following query:

```graphql
query {
  product(stockcode: "1-1") {
    description
    name
    price
    stockcode
    tags {
      category
      kind
      text
      value
    }
  }
}
```

Which will result in the following response:

```json
{
  "data": {
    "product": {
      "description": "Laptop with Intel i5, 16Gb RAM and 1TB SSD",
      "name": "Intel Laptop",
      "price": 1900,
      "stockcode": "1-1",
      "tags": [
        {
          "category": "New",
          "kind": "Information",
          "text": "New",
          "value": ""
        }
      ]
    }
  }
}
```

## DataLoader to avoid the N+1 problem

Fetching multiple products can result in the N+1 problem, which will cause multiple database queries to be executed,
due to GraphQL field resolvers being atomic and not knowing about the query as a whole.
This can be avoided by using a DataLoader, which will batch all requests together into one query to the database.

Data Loaders were implemented for both Product and ProductTag models.

## Managing Products

The Api project also exposes REST API endpoints for managing products. Swagger UI can be used to interact with the API.

## Security

The REST API is secured using JWT tokens.

In local development environment, a token can be generated using the `user-jwt` tool:

```bash
dotnet user-jwts create --audience productdetails-api --role Admin --claim "AdminId=1"
```
