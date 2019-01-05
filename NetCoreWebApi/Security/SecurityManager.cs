using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PtcApi.Data;
using PtcApi.Model;

namespace PtcApi.Security
{
    public class SecurityManager: ISecurityManager
    {
        private readonly PtcDbContext _ptcDbContext;
        private readonly JwtSettings _jwtSettings;
        public SecurityManager(PtcDbContext ptcDbContext, JwtSettings jwtSettings)
        {
            _ptcDbContext = ptcDbContext;
            _jwtSettings = jwtSettings;
        }

        //Validates username and password and return a Authorization object if the credentials are right.
        public AppUserAuth ValidateUser(AppUser user)
        {
            try
            {
                var appUserAuth = new AppUserAuth();

                // Attempt to validate user: if username and password are correct, it will return an user.
                var authUser = _ptcDbContext.Users.FirstOrDefault(u =>
                     u.UserName.ToLower() == user.UserName.ToLower()
                     && u.Password == user.Password);

                if (authUser != null)
                {
                    // Build User Security Object
                    appUserAuth = BuildUserAuthObject(authUser);
                }

                return appUserAuth;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception trying to authenticate user.", ex);
            }
        }

        protected string BuildJwtToken(AppUserAuth authUser)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

            // Create standard JWT claims
            var jwtClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, authUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),    
                new Claim("isAuthenticated", authUser.IsAuthenticated.ToString().ToLower()),
            };
            
            // Add custom claims
            foreach (var claim in authUser.Claims)
            {
                jwtClaims.Add(new Claim(claim.ClaimType, claim.ClaimValue));
            }

            // Create the JwtSecurityToken object
            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: jwtClaims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.MinutesToExpiration),
                signingCredentials: new SigningCredentials(key,SecurityAlgorithms.HmacSha256)
            );

            // Create a string representation of the Jwt token
            return new JwtSecurityTokenHandler().WriteToken(token); ;
        }

        // Creates the Authorization object
        protected AppUserAuth BuildUserAuthObject(AppUser authUser)
        {
            // Set User Properties
            var appUserAuth = new AppUserAuth
            {
                UserName = authUser.UserName,
                IsAuthenticated = true, // If enters here is bacause the user was authenticated, so we can set this property to true.
                BearerToken = Guid.NewGuid().ToString()
            };

            // Get all claims for this user
            appUserAuth.Claims = GetUserClaims(authUser);
            appUserAuth.BearerToken = BuildJwtToken(appUserAuth);

            return appUserAuth;
        }

        // Get user claims
        protected List<AppUserClaim> GetUserClaims(AppUser authUser)
        {
            try
            {
                var claims = _ptcDbContext.Claims.Where(x => x.UserId == authUser.UserId).ToList();
                return claims;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception trying to retrieve user claims.", ex);
            }
        }
    }
}
