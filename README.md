# Product Details

Experiments with GraphQL, FastEndpoints and RulesEngine.

### ProductDetails.Api

This project is a GraphQL API that exposes an endpoint for querying product details.
It is built using the [HotChocolate](https://hotchocolate.io/) library.

It also exposes REST API endpoints for managing products.

### Security

The REST API is secured using JWT tokens and Swagger UI can be used to interact with the API.

In local development environment, a token can be generated using the `user-jwt` tool:

```bash
dotnet user-jwts create --audience productdetails-api --role Admin --claim "AdminId=1"
```
