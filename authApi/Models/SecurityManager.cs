using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace authApi.Models
{
  public class SecurityManager
  {
    private readonly AuthDbContext ctx;
    private readonly JwtSettings _settings;
    public SecurityManager(AuthDbContext ctx, JwtSettings settings)
    {
      this.ctx = ctx;
      this._settings = settings;
    }
    public AppUserAuth ValidateUser(AppUser appUser)
    {
        var user = ctx.AppUsers
            .Where(u=> u.UserName == appUser.UserName &&
                   u.Password == appUser.Password).FirstOrDefault();
        
        if (user == null) {
            return new AppUserAuth();
        }

        var ret = new AppUserAuth {
          UserName = user.UserName,
          IsAuthenticated = true
        };

        ret.BearerToken = BuildJwtToken(ret);
        
        return ret;
    }

    protected string BuildJwtToken(AppUserAuth authUser){
      var key = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(_settings.Key)
      );

      List<Claim> jwtClaims = new List<Claim>();
      jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, authUser.UserName));
      jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
      jwtClaims.Add(new Claim("isAuthenticated",
        authUser.IsAuthenticated.ToString().ToLower()));

      var token = new JwtSecurityToken(
        issuer: _settings.Issuer,
        audience: _settings.Audience,
        claims: jwtClaims,
        notBefore: DateTime.UtcNow,
        expires: DateTime.UtcNow.AddMinutes(_settings.MinutosToExpiration),
        signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
      );

      return new JwtSecurityTokenHandler().WriteToken(token);
    }
  }
}