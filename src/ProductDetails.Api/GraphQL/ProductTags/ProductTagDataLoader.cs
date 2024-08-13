﻿using ProductDetails.Domain.Tags;

namespace ProductDetails.Api.GraphQL.ProductTags;

public sealed class ProductTagDataLoader(
    ITagRepository tagRepository,
    IBatchScheduler batchScheduler,
    DataLoaderOptions? options = null) : BatchDataLoader<string, TagModel[]>(batchScheduler, options)
{
    private readonly ITagRepository _tagRepository = tagRepository;

    protected override async Task<IReadOnlyDictionary<string, TagModel[]>> LoadBatchAsync(
        IReadOnlyList<string> keys,
        CancellationToken cancellationToken)
    {
        var productTags = await _tagRepository.GetByStockcodeAsync([.. keys], cancellationToken);

        return productTags.ToDictionary(p => p.Stockcode, p =>
            p.Tags.Select(t => new TagModel(t.Kind.ToString(), t.Category.ToString(), t.Text, t.Value))
                  .ToArray());
    }
}