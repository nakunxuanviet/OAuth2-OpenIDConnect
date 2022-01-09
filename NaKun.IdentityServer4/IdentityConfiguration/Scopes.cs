using IdentityServer4.Models;
using System.Collections.Generic;

namespace NaKun.IdentityServer4.IdentityConfiguration
{
    public class Scopes
    {
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new[]
            {
                new ApiScope("weatherApi.read", "Read Access to Weather API"),
                new ApiScope("weatherApi.write", "Write Access to Weather API"),
             };
        }
    }
}
