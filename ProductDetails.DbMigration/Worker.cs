namespace ProductDetails.DbMigration;

public class Worker(IHostApplicationLifetime hostApplicationLifetime, ILogger<Worker> logger) : BackgroundService
{
    private readonly IHostApplicationLifetime _hostApplicationLifetime = hostApplicationLifetime;
    private readonly ILogger<Worker> _logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Running migrations...");

        await DB.MigrateAsync();

        _logger.LogInformation("Migrations complete.");

        _hostApplicationLifetime.StopApplication();
    }
}
