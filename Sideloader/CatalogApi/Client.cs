using RestSharp;
using System.Threading.Tasks;

namespace Sideloader.CatalogApi
{
    /// <summary>
    /// HTTP API Client for accessing the add-in catalog
    /// </summary>
    public class Client
    {
        public string BaseUri { get; private set; }

        private const string defaultBaseUri = "http://localhost:8083/api";

        public Client() : this(defaultBaseUri)
        {

        }

        public Client(string baseUri)
        {
            BaseUri = baseUri;
        }

        public async Task<CollectionResponse<Package>> GetIndexAsync(string packageType)
        {
            var client = CreateClient();
            var path = string.Format("/packages/{0}", packageType);
            var request = new RestRequest(path);

            return await client.GetAsync<CollectionResponse<Package>>(request);
        }

        private static string UserAgent()
        {
            var info = System.Reflection.Assembly.GetExecutingAssembly().GetName();
            return string.Format("{0}/{1}", info.Name, info.Version.ToString());
        }

        private RestClient CreateClient()
        {
            var client = new RestClient(BaseUri)
            {
                UserAgent = UserAgent()
            };
            client.UseJson();
            return client;
        }


    }
}
