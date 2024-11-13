using IdentityServer4.Models;

namespace Server
{
    public class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<string> { "role" }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new[]
            {
                new ApiScope("Lab5API.read"),
                new ApiScope("Lab5API.write")
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new[]
            {
                new ApiResource("Lab5API")
                {
                    Scopes = new List<string> { "Lab5API.read", "Lab5API.write" },
                    ApiSecrets = new List<Secret> { new Secret("Client1_Secret".Sha256()) },
                    UserClaims = new List<string> { "role" }
                }
            };

        public static IEnumerable<Client> Clients =>
            new[]
            {
                new Client
                {
                    ClientId = "password_client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = { new Secret("Client1_Secret".Sha256()) },
                    AllowedScopes = { "openid", "profile", "Lab5", "Lab5API.read", "Lab5API.write" },
                    //AllowOfflineAccess = true
                },
                new Client()
                {
                    ClientId = "interactive",
                    ClientSecrets = { new Secret("Client1_Secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://localhost:5443/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:5443/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:5443/signout-callback-oidc" },
                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "Lab5API.read", "Lab5API.write" },
                    RequirePkce = false,
                    RequireConsent = true,
                    AllowPlainTextPkce = false
                }
            };
    }   
}
