namespace Tracking.Api.Utils
{
    public static class Constants
    {
        public const string JsonFilePath = "appsettings.json";
        public const string JsonEnviromentFilePath = "appsettings.{0}.json";
        public const string EnviromentVariable = "ASPNETCORE_ENVIRONMENT";
        public const string StarAppMessage = "*************** START APPLICATION ***************";
        public const string ConfigAppMessge = "Configuring application...";
        public const string StartingAppMesage = "Starting application...";
        public const string EndingAppMessage = "Application terminated unexpectedly!";
        public const string EndAppMessage = "*************** END APPLICATION ***************";
        public const string ContentTypeJson = "application/json";
        public const string ContentTypeTextHtml = "text/html";
        public const string CorsPolicyName = "CorsPolicy";
        public const string WelcomePath = "/";
        public const string HealthPath = "/health";
        public const string ExternalHealthPath = "/api/v1/health";
        public const string WelcomeAPI = "<div style=\"text-align: center;padding: 10px;height: 100px;background-color: #FD620F; color: white; font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;\"> <h1>{0}</h1></div>";
        public const string ConnectionString = "Tracking";
        public const string JwtSettings = "JwtSettings";
        public const string ExternalServicesSettings = "ExternalServices";
        public const string GlobalOAuthPolicyName = "GlobalOAuthPolicy";
    }
}
