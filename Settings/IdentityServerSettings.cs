using System;
using System.Collections.Generic;
using IdentityModel;
using IdentityServer4.Models;

namespace UserIdentity.Settings
{
    public class IdentityServerSettings
    {
        public IReadOnlyCollection<ApiScope> ApiScopes { get; set; } = Array.Empty<ApiScope>();
        public IReadOnlyCollection<Client> Clients { get; init; }
        public IReadOnlyCollection<IdentityResource> IdentityResources =>
         new IdentityResource[]
         {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource(
            "roles",
            new [] { JwtClaimTypes.Role }
            ),
            new IdentityResource("IsApproved",new [] { "IsApproved" }),

         };
    }
}