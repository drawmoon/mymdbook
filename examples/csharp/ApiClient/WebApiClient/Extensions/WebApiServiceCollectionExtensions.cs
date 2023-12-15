using WebApiClient;
using WebApiClient.Extensions.HttpClientFactory;

public static class WebApiServiceCollectionExtensions
{
    public static IServiceCollection AddApiClient<TInterface>(this IServiceCollection services, Action<ApiClientOption>? options = null)
        where TInterface : class, IHttpApi
    {
        var apiClientOption = new ApiClientOption();
        options?.Invoke(apiClientOption);

        // if (Configuration.GetValue<bool>("UseSpringCloud"))
        // {
        //     services.AddDiscoveryTypedClient<IUsersWebApi>(options =>
        //         {
        //             options.HttpHost = new Uri("http://ServerPpL7XK");
        //             options.FormatOptions.DateTimeFormat = DateTimeFormats.ISO8601_WithoutMillisecond;
        //         })
        //         .ConfigureHttpClient(SetDefaultRequestHeaders);
        // }
        // else
        {
            services.AddHttpApiTypedClient<TInterface>(config =>
            {
                config.HttpHost = apiClientOption.HttpHost ?? GetDefaultHttpHost();
                config.FormatOptions.DateTimeFormat = DateTimeFormats.ISO8601_WithoutMillisecond;
            })
                .ConfigureHttpClient(SetDefaultRequestHeaders);
        }

        return services;
    }

    private static void SetDefaultRequestHeaders(IServiceProvider serviceProvider, HttpClient client)
    {
        var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
        var httpContext = httpContextAccessor.HttpContext;

        if (httpContext.Request.Headers.TryGetValue("Authorization", out var authorization))
        {
            client.DefaultRequestHeaders.Add("Authorization", authorization.ToString());
        }

        if (httpContext.Request.Headers.TryGetValue("Cookie", out var cookie))
        {
            client.DefaultRequestHeaders.Add("Cookie", cookie.ToString());
        }
    }

    private static Uri GetDefaultHttpHost()
    {
        return new Uri("http://localhost");
    }
}

public class ApiClientOption
{
    public Uri HttpHost { get; set; }
}