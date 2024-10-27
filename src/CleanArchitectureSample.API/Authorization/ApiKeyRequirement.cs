using Microsoft.AspNetCore.Authorization;

namespace CleanArchitectureSample.API.Authorization;

public class ApiKeyRequirement : IAuthorizationRequirement
{
    public string ApiKey { get; set; } = String.Empty;
}
