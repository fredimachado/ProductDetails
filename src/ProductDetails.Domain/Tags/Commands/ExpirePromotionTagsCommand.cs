﻿using MediatR;
using Microsoft.Extensions.Logging;

namespace ProductDetails.Domain.Tags.Commands;

public sealed record ExpirePromotionTagsCommand(string PromotionId, string Stockcode) : IRequest;

internal sealed class ExpirePromotionTagsCommandHandler(ITagRepository tagRepository, ILogger<ExpirePromotionTagsCommandHandler> logger) : IRequestHandler<ExpirePromotionTagsCommand>
{
    private readonly ITagRepository _tagRepository = tagRepository;
    private readonly ILogger _logger = logger;

    public async Task Handle(ExpirePromotionTagsCommand request, CancellationToken cancellationToken)
    {
        var productTag = await _tagRepository.GetByStockcodeAsync(request.Stockcode, cancellationToken);

        if (productTag == null)
        {
            _logger.LogWarning("Product tag not found for stockcode: {Stockcode}", request.Stockcode);
            return;
        }

        productTag.DisableTags(request.PromotionId);

        await _tagRepository.SaveAsync(productTag, cancellationToken);
    }
}
