using Aspirant.Hosting;

namespace Aspire.Hosting;

public static class ResourceBuilderExtensions
{
    public static IResourceBuilder<TDestination> WithReferenceWait<TDestination>(this IResourceBuilder<TDestination> builder, IResourceBuilder<IResourceWithConnectionString> source, string? connectionName = null, bool optional = false)
        where TDestination : IResourceWithEnvironment
    {
        return builder.WithReference(source, connectionName, optional)
                      .WaitFor(source);
    }
}
