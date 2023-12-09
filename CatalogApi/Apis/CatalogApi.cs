using CatalogApi.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CatalogApi.Api;

public static class CatalogApi
{
    public static IEndpointRouteBuilder MapCatalogApi(this IEndpointRouteBuilder app)
    {
        // Routes for querying catalog items.
        app.MapGet("/items", GetAllItems);

        return app;
    }

    public static async Task<Results<Ok<PaginatedItems<CatalogItem>>, BadRequest<string>>> GetAllItems(
        [AsParameters] PaginationRequest paginationRequest,
        [AsParameters] CatalogServices services)
    {
        using var activity = TelemetryConstants.ActivitySource.StartActivity("CatalogApi.GetAllItems");

        var counter = TelemetryConstants.Meter.CreateCounter<int>(
            "catalogapi.catalogitems.getallitems"
        );

        counter.Add(1);

        var pageSize = paginationRequest.PageSize;
        var pageIndex = paginationRequest.PageIndex;

        var totalItems = await services.Context.CatalogItems
            .LongCountAsync();

        var itemsOnPage = await services.Context.CatalogItems
            .OrderBy(c => c.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();

        itemsOnPage = ChangeUriPlaceholder(services.Options.Value, itemsOnPage);

        return TypedResults.Ok(new PaginatedItems<CatalogItem>(pageIndex, pageSize, totalItems, itemsOnPage));
    }

    private static List<CatalogItem> ChangeUriPlaceholder(CatalogOptions options, List<CatalogItem> items)
    {
        foreach (var item in items)
        {
            item.PictureUri = options.PicBaseUrl!.Replace("[0]", item.Id.ToString());
        }

        return items;
    }
}