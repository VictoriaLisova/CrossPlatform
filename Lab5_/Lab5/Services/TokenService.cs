using IdentityModel.Client;
using System.Net;

namespace Lab5.Services
{
    public class TokenService
    {
        private readonly HttpClient _httpClient;
        public string AccessToken { get; set; }
        public TokenService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpStatusCode> GetToken()
        {
            var disco = await _httpClient.GetDiscoveryDocumentAsync("https://localhost:5443");

            if (disco.IsError)
            {
                Console.WriteLine("can`t get token");
            }

            if (!disco.IsError)
            {
                var tokenResponce = await _httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                {
                    Address = disco.TokenEndpoint,
                    ClientId = "mashine_to_mashine_client",
                    ClientSecret = "Client2_Secret",
                    Scope = "Lab6API"
                });

                if (!tokenResponce.IsError)
                {
                    var api = new HttpClient();

                    Console.WriteLine(tokenResponce.AccessToken);

                    api.SetBearerToken(tokenResponce.AccessToken);

                    AccessToken = tokenResponce.AccessToken;

                    var responce = await api.GetAsync("https://localhost:7277/identity");
                    return responce.StatusCode;
                }
            }
            return HttpStatusCode.Unauthorized;
        }
    }
}
