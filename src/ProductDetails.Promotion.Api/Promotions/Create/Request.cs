namespace ProductDetails.Promotion.Api.Promotions.Insert;

public record Request(string Stockcode, decimal PromotionalPrice, DateTimeOffset StartDateUtc, DateTimeOffset EndDateUtc);
