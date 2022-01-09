using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NaKun.WebAPI.Client.Services
{
    public class IdentityServerService : IIdentityServerService
    {
        private DiscoveryDocumentResponse _discoveryDocument { get; set; }
        public IdentityServerService()
        {
            using (var client = new HttpClient())
            {
                _discoveryDocument = client.GetDiscoveryDocumentAsync("https://localhost:5001/.well-known/openid-configuration").Result;
            }
        }

        public async Task<TokenResponse> GetToken(string apiScope)
        {
            using (var client = new HttpClient())
            {
                var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                {
                    Address = _discoveryDocument.TokenEndpoint,
                    ClientId = "weatherApi",
                    Scope = apiScope,
                    ClientSecret = "NaKun"
                });

                if (tokenResponse.IsError)
                {
                    throw new Exception("Token Error");
                }
                return tokenResponse;
            }
        }
    }
}
