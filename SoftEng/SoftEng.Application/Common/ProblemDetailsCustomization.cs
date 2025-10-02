using Microsoft.Extensions.DependencyInjection;

namespace SoftEng.Application.Common;

public static class ProblemDetailsCustomization
{
    public static IServiceCollection AddProblemDetailsCustomization(this IServiceCollection services)
    {
        services.AddProblemDetails(o =>
        {
            o.CustomizeProblemDetails = ctx =>
            {
                if (!ctx.HttpContext.Response.Headers.TryGetValue("X-Correlation-Id", out var cid))
                    cid = Guid.NewGuid().ToString();
                ctx.ProblemDetails.Extensions["correlationId"] = cid.ToString();
                ctx.ProblemDetails.Extensions["traceId"] = ctx.HttpContext.TraceIdentifier;
            };
        });
        return services;
    }
}
