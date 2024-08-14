using System.Reflection;

namespace ProductDetails.Infrastructure.Messaging;

internal static class ExchangeNameProvider
{
    public static string Get(string exchange)
    {
        return !string.IsNullOrWhiteSpace(exchange) ? exchange : Assembly.GetEntryAssembly()!.GetName().Name!;
    }
}
