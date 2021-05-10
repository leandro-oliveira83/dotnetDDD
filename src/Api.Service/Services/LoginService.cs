using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Api.Domain.Dtos;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services.Users;
using Api.Domain.Repository;
using Api.Domain.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Api.Service.Services
{
  public class LoginService : ILoginService
  {
    private IUserRepository _repository;
    private SigningConfigurations _signingConfigurations;
    private IConfiguration _configuration { get; }
    public LoginService(IUserRepository repository,
                        SigningConfigurations signingConfigurations,
                        IConfiguration configuration)
    {
      _repository = repository;
      _signingConfigurations = signingConfigurations;
      _configuration = configuration;
    }

    public async Task<object> FindByLogin(LoginDto user)
    {
      var baseUser = new UserEntity();
      if (user != null && !string.IsNullOrWhiteSpace(user.Email))
      {
        baseUser = await _repository.FindByLogin(user.Email);
        if (baseUser == null)
        {
          return new ResponseDto()
          {
            Success = false,
            Message = "Usuário ou senha inválidos."
          };
        }

        var token = new JwtBuilder()
                  .WithUserManager(_repository)
                  .WithSigningConfigurations(_signingConfigurations)
                  .WithJwtSettings(new TokenConfigurations()
                  {
                    Audience = Environment.GetEnvironmentVariable("Seconds"),
                    Issuer = Environment.GetEnvironmentVariable("Issuer"),
                    Seconds = Convert.ToInt32(Environment.GetEnvironmentVariable("Seconds"))
                  })
                  .WithEmail(user.Email)
                  .WithJwtClaims()
                  //.WithUserClaims()
                  //.WithUserRoles()
                  .BuildTokenResponse();

        return new
        {
          success = true,
          message = "Usuário autenticado com sucesso.",
          data = token
        };
      }

      return new
      {
        authenticated = false,
        message = "Falha ao autenticar"
      };
    }
  }
}