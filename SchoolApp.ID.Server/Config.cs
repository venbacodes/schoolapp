using IdentityServer4.Models;
using System.Collections.Generic;

namespace SchoolApp.ID.Server
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("scope1"),
                new ApiScope("scope2"),
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // interactive client using code flow + pkce
                new Client
                {
                    ClientId = "school_app_client",
                    ClientSecrets = { new Secret("school_app_secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://localhost:44301/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:44301/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:44301/signout-callback-oidc" },
                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile" },
                    AlwaysIncludeUserClaimsInIdToken = true,
                },
            };
    }
}