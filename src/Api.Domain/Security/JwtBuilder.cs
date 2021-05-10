using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Api.Domain.Repository;
using Api.Domain.Entities;
using Api.Domain.Dtos;

namespace Api.Domain.Security
{
  public class JwtBuilder<TIdentityUser, TSigningConfigurations>
          where TIdentityUser : IUserRepository
          where TSigningConfigurations : SigningConfigurations
  {
    private IUserRepository _userManager;
    private TokenConfigurations _appJwtSettings;
    private UserEntity _user;
    private ICollection<Claim> _userClaims;
    private ICollection<Claim> _jwtClaims;
    private ClaimsIdentity _identityClaims;
    private SigningConfigurations _signingConfigurations;

    public JwtBuilder<TIdentityUser, TSigningConfigurations> WithUserManager(IUserRepository userManager)
    {
      _userManager = userManager ?? throw new ArgumentException(nameof(userManager));
      return this;
    }

    public JwtBuilder<TIdentityUser, TSigningConfigurations> WithSigningConfigurations(SigningConfigurations signingConfigurations)
    {
      _signingConfigurations = signingConfigurations ?? throw new ArgumentException(nameof(signingConfigurations));
      return this;
    }

    public JwtBuilder<TIdentityUser, TSigningConfigurations> WithJwtSettings(TokenConfigurations appJwtSettings)
    {
      _appJwtSettings = appJwtSettings ?? throw new ArgumentException(nameof(appJwtSettings));
      return this;
    }

    public JwtBuilder<TIdentityUser, TSigningConfigurations> WithEmail(string email)
    {
      if (string.IsNullOrEmpty(email)) throw new ArgumentException(nameof(email));

      _user = _userManager.FindByLogin(email).Result;
      _userClaims = new List<Claim>();
      _jwtClaims = new List<Claim>();
      _identityClaims = new ClaimsIdentity();

      return this;
    }

    public JwtBuilder<TIdentityUser, TSigningConfigurations> WithJwtClaims()
    {
      _jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, _user.Id.ToString()));
      _jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Email, _user.Email));
      _jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
      _jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
      _jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

      _identityClaims.AddClaims(_jwtClaims);

      return this;
    }

    /*public JwtBuilder<TIdentityUser> WithUserClaims()
    {
      _userClaims = _userManager.GetClaimsAsync(_user).Result;
      _identityClaims.AddClaims(_userClaims);

      return this;
    }

    public JwtBuilder<TIdentityUser> WithUserRoles()
    {
      var userRoles = _userManager.GetRolesAsync(_user).Result;
      userRoles.ToList().ForEach(r => _identityClaims.AddClaim(new Claim("role", r)));

      return this;
    }*/

    public string BuildToken()
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
      {
        Issuer = _appJwtSettings.Issuer,
        Audience = _appJwtSettings.Audience,
        Subject = _identityClaims,
        Expires = DateTime.UtcNow.AddHours(_appJwtSettings.Seconds),
        SigningCredentials = _signingConfigurations.SigningCredentials,
      });

      return tokenHandler.WriteToken(token);
    }

    public ResponseDtoToken BuildTokenResponse()
    {
      DateTime createDate = DateTime.UtcNow;
      DateTime expirationDate = createDate.AddHours(_appJwtSettings.Seconds);

      var user = new ResponseDtoToken
      {
        Authenticated = true,
        AccessToken = BuildToken(),
        Created = createDate,
        Expiration = expirationDate,
        UserEmail = _user.Email,
        UserName = _user.Name
        /*UserToken = new UserToken<TKey>
        {
          Id = _user.Id,
          Email = _user.Email,
          Claims = _userClaims.Select(c => new UserClaim { Type = c.Type, Value = c.Value })
        }*/
      };

      return user;
    }

    private static long ToUnixEpochDate(DateTime date)
        => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
            .TotalSeconds);
  }

  public sealed class JwtBuilder : JwtBuilder<IUserRepository, SigningConfigurations> { }
}