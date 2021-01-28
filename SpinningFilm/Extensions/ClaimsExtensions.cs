using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace SpinningFilm.Extensions
{
    public static class ClaimsExtensions
    {
        #region IPrincipal Extensions
        //
        // Summary:
        //     Determines whether the current user is acting as another user.
        //
        // Returns:
        //     The claim value.
        public static bool IsCloaked(this IPrincipal principal)
        {
            return ((ClaimsPrincipal)principal).HasClaim(x => x.Type == ClaimTypes.UserData);
        }

        public static bool HasRole(this IPrincipal principal)
        {
            return ((ClaimsPrincipal)principal).HasClaim(x => x.Type == ClaimTypes.Role);
        }

        public static string NameId(this IIdentity identity)
        {
            return ((ClaimsIdentity)identity).FindFirst(ClaimTypes.NameIdentifier).Value;            
        }

        public static string GetClaimValue(this IIdentity identity, string type)
        {
            try
            {
                return ((ClaimsIdentity)identity).FindFirst(type).Value;
            }
            catch
            {
                return "";
            }
        }
        #endregion

        #region ClaimsIdentity Extensions
        public static void RemoveClaims(this ClaimsIdentity identity, IEnumerable<Claim> claims)
        {
            var claimsArray = claims.ToArray();

            for (int i = claimsArray.Count() - 1; i >= 0; i--)
            {
                identity.RemoveClaim(claimsArray[i]);
            }
        }

        public static void RemoveClaims(this ClaimsIdentity identity, string claimType)
        {
            IEnumerable<Claim> claims = identity.FindAll(claimType);

            foreach (Claim claim in claims)
                identity.RemoveClaim(claim);
        }

        public static void RemoveAllClaims(this ClaimsIdentity identity)
        {
            RemoveClaims(identity, identity.Claims);
        }

        public static void ChangeClaimTypes(this ClaimsIdentity identity, string fromClaimType, string toClaimType)
        {
            IEnumerable<Claim> fromClaims = identity.FindAll(fromClaimType);

            foreach (Claim claim in fromClaims)
            {
                identity.AddClaim(new Claim(toClaimType, claim.Value));
                identity.RemoveClaim(claim);
            }
        }

        public static void SwapClaimTypes(this ClaimsIdentity identity, string firstClaimType, string secondClaimType)
        {
            IEnumerable<Claim> firstClaims = identity.FindAll(firstClaimType);
            IEnumerable<Claim> secondClaims = identity.FindAll(secondClaimType);

            foreach (Claim claim in firstClaims)
            {
                identity.AddClaim(new Claim(secondClaimType, claim.Value));
                identity.RemoveClaim(claim);
            }

            foreach (Claim claim in secondClaims)
            {
                identity.AddClaim(new Claim(firstClaimType, claim.Value));
                identity.RemoveClaim(claim);
            }
        }

        public static void AddRolesAsClaims(this ClaimsIdentity identity, List<string> roles)
        {
            foreach (string role in roles)
            {
                if (!identity.HasClaim(ClaimTypes.Role, role))
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, role, "", ""));
                }
            }
        }

        private static void AddClaimIfUnique(this ClaimsIdentity identity, string type, string value)
        {
            if (!string.IsNullOrWhiteSpace(type) && !string.IsNullOrWhiteSpace(value))
            {
                if (!identity.HasClaim(type, value))
                {
                    identity.AddClaim(new Claim(type, value, "string", ""));
                }
            }
        }
        #endregion
    }
}
