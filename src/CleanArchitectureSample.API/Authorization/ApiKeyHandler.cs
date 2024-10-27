using Microsoft.AspNetCore.Authorization;

namespace CleanArchitectureSample.API.Authorization;

public class ApiKeyHandler(IHttpContextAccessor httpContextAccessor) : AuthorizationHandler<ApiKeyRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor ??
        throw new ArgumentNullException(nameof(httpContextAccessor));

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ApiKeyRequirement requirement)
    {
        string apiKey = _httpContextAccessor.HttpContext?.Request.Headers[ApiConstants.Configuration.ApiKeyHeader].ToString() ?? String.Empty;

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            context.Fail();
            return Task.CompletedTask;
        }

        // TODO: Implement JwtSecurityToken based on incoming apikey
        if (!requirement.ApiKey.Equals(apiKey))
        {
            context.Fail();
            return Task.CompletedTask;
        }

        context.Succeed(requirement);
        return Task.CompletedTask;
    }
}
