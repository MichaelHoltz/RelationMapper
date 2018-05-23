using TmdbWrapper.Utilities;
using System.Threading.Tasks;
namespace TmdbWrapper
{
    /// <summary>
    /// The static that wraps The movie database service.
    /// It should be initilised with your API_KEY.
    /// You can apply for an API_KEY at www.TheMovieDb.org
    /// </summary>
    public static partial class TheMovieDb
    {
        /// <summary>
        /// The apikey that is used in all requests.
        /// </summary>
        internal static string ApiKey { get; private set; }
        /// <summary>
        /// Language all the request uses if entered.
        /// </summary>
        internal static string Language { get; private set; }

        internal static bool UseSecureConnections { get; set; }
        internal static Configuration.Configuration Configuration { get; private set; }

        public static System.Uri GetConfigurationBaseUrl()
        {
            return Configuration.ImageConfiguration.BaseUrl;
        }
        public static System.Uri GetConfigurationSecureBaseUrl()
        {
            return Configuration.ImageConfiguration.SecureBaseUrl;
        }

        /// <summary>
        /// Initialises the wrapper.
        /// </summary>
        /// <param name="apiKey">The apikey the requests will use.</param>  
        /// <param name="useSecureConnections">Inidicates if a secure connection should be used.</param>
        public static async Task<bool> Initialise(string apiKey, bool useSecureConnections = true) 
        {
            ApiKey = apiKey;
            UseSecureConnections = useSecureConnections;
            var config = await GetConfig();
            Configuration = config; // Assign
            Extensions.Initialize(useSecureConnections ? config.ImageConfiguration.SecureBaseUrl : config.ImageConfiguration.BaseUrl);
            while (Configuration == null)
            {
                System.Threading.Thread.Sleep(100);
            }
            return true; // Done
        }
        // TmdbWrapper.Configuration.Configuration config;
        /// <summary>
        /// Initialises the wrapper.
        /// </summary>
        /// <param name="apiKey">The apikey the request will use.</param>       
        /// <param name="language">The language the requests will use.</param>
        /// <param name="useSecureConnections">Inidicates if a secure connection should be used.</param>
        public static async Task<bool> Initialise(string apiKey, string language, bool useSecureConnections = true)
        {
            Language = language;
            await Initialise(apiKey, useSecureConnections);
            return true;
        }
        private static async Task<Configuration.Configuration> GetConfig()
        {
            Request<Configuration.Configuration>.Initialize(UseSecureConnections);
            var request = new Request<Configuration.Configuration>("configuration");
            var config = await request.ProcesRequestAsync();
            return config;
        }
    }
}
