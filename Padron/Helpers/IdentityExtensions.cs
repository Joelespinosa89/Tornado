using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace Padron.Helpers
{
    public static class IdentityExtensions
    {
        public static string GetNombreCompleto(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("NombreCompleto");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetUserCode(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("UserCode");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetUserRol(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("UserRol");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}