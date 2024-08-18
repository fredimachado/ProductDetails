using ProductDetails.Domain.Messages;
using ProductDetails.Domain.Messaging;
using ProductDetails.Domain.Products;
using ProductDetails.Domain.Promotions;

namespace ProductDetails.Promotion.Worker;

public class Worker(
    IPromotionRepository promotionRepository,
    IProductRepository productRepository,
    IBusPublisher publisher,
    IConfiguration configuration,
    ILogger<Worker> logger) : BackgroundService
{
    private readonly IPromotionRepository _promotionRepository = promotionRepository;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IBusPublisher _publisher = publisher;
    private readonly IConfiguration _configuration = configuration;
    private readonly ILogger<Worker> _logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var fetchFrequencyInSeconds = _configuration.GetValue("FetchFrequencyInSeconds", 10);

        _logger.LogInformation("Promotion Worker started at: {Time}. Fetch frequency: {Frequency}s", DateTimeOffset.Now, fetchFrequencyInSeconds);

        while (!stoppingToken.IsCancellationRequested)
        {
            await FetchAndPublishExpiredPromotions(stoppingToken);

            await FetchAndPublishPendingPromotions(stoppingToken);

            await Task.Delay(fetchFrequencyInSeconds * 1000, stoppingToken);
        }
    }

    private async Task FetchAndPublishExpiredPromotions(CancellationToken stoppingToken)
    {
        var expiredPromotions = await _promotionRepository.GetExpiredPromotionsAsync(stoppingToken);

        foreach (var promotion in expiredPromotions)
        {
            _logger.LogInformation("Publishing expired promotion: {@Promotion}", promotion);

            try
            {
                await _promotionRepository.SetPublishedExpiryAsync(promotion.PromotionId!, stoppingToken);

                await _publisher.Publish(new ProductPromotionExpiredMessage(
                    promotion.PromotionId!,
                    promotion.Stockcode));

                _logger.LogInformation("Expired promotion published: {PromotionId}", promotion.PromotionId);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error publishing expired promotion: {@Promotion}", promotion);
                throw;
            }
        }
    }

    private async Task FetchAndPublishPendingPromotions(CancellationToken stoppingToken)
    {
        var pendingPromotions = await _promotionRepository.GetPendingPromotionsAsync(stoppingToken);

        foreach (var promotion in pendingPromotions)
        {
            var product = await _productRepository.GetByStockcodeAsync(promotion.Stockcode, stoppingToken);
            if (product == null)
            {
                _logger.LogWarning("Product with stockcode {Stockcode} not found", promotion.Stockcode);
                continue;
            }

            _logger.LogInformation("Publishing promotion: {@Promotion}", promotion);

            try
            {
                await _promotionRepository.SetPublishedDateAsync(promotion.PromotionId!, stoppingToken);

                await _publisher.Publish(new ProductPromotionMessage(
                    promotion.PromotionId!,
                    promotion.Stockcode,
                    product.Price,
                    promotion.PromotionalPrice,
                    promotion.EndDateUtc));

                _logger.LogInformation("Promotion published: {PromotionId}", promotion.PromotionId);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error publishing promotion: {@Promotion}", promotion);
                throw;
            }
        }
    }
}
