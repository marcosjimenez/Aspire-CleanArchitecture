namespace CleanArchitectureSample.API
{
    public static class ApiConstants
    {
        public static class Configuration
        {
            public const string DefaultSettingsFile = "appsettings.json";
            public const string ApiKeySection = "ApiKey";
            public const string ApiKeyHeader = "ApiKey";
        }

        public static class Security
        {
            public const string ApiKeyPolicyName = "ApiKeyPolicy";
        }
    }
}
