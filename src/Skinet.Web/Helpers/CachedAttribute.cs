using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Skinet.Core.Interfaces;

namespace Skinet.Web.Helpers;

public class CachedAttribute : Attribute, IAsyncActionFilter
{
    private readonly int _timeToLiveInSeconds;

    public CachedAttribute(int timeToLiveInSeconds)
    {
        _timeToLiveInSeconds = timeToLiveInSeconds;
    }
    
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        //Get cache service
        var cachedService = context.HttpContext.RequestServices
            .GetRequiredService<IResponseCacheService>();

        var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);

        var cachedResponse = await cachedService.GetCachedResponseAsync(cacheKey);

        if (!string.IsNullOrEmpty(cachedResponse))
        {
            var contentResult = new ContentResult
            {
                Content = cachedResponse,
                ContentType = "application/json",
                StatusCode = 200
            };
            context.Result = contentResult;
            return;
        }

        var executedContext = await next(); //Move to controller

        if (executedContext.Result is OkObjectResult okObjectResult)
        {
            await cachedService.CacheResponseAsync(cacheKey, okObjectResult.Value,
                TimeSpan.FromSeconds(_timeToLiveInSeconds));
        }
    }

    private string GenerateCacheKeyFromRequest(HttpRequest request)
    {
        var keyBuilder = new StringBuilder();
        keyBuilder.Append($"{request.Path}");

        foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
        {
            keyBuilder.Append($"|{key}-{value}");
        }

        return keyBuilder.ToString();
    }
}
