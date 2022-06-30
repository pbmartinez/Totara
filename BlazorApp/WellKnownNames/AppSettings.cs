namespace BlazorApp.WellKnownNames
{
    public static class AppSettings
    {
        /// <summary>
        /// Api base url
        /// </summary>
        public static string ApiBaseUrl => "ApiBaseUrl";
        /// <summary>
        /// Azure json object for configuration
        /// </summary>
        public static string AzureAd => "AzureAd";

        /// <summary>
        /// Name of http client for identification
        /// </summary>
        public static string HttpClientApi => "HttpClientApi";

        /// <summary>
        /// Scope por client app can accesses to api as if it were the user
        /// </summary>
        public static string ScopeApiAccess => "ScopeyApiAccess";
        
        /// <summary>
        /// Max times to retry on api call error
        /// </summary>
        public static string PollyRetryCount => "PollyRetryCount";


    }
}
