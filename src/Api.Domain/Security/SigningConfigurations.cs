using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Api.Domain.Security
{
  public class SigningConfigurations
  {
    public SecurityKey Key { get; set; }
    public SigningCredentials SigningCredentials { get; set; }

    public SigningConfigurations()
    {
      using (var provider = new RSACryptoServiceProvider(2048))
      {
        Key = new RsaSecurityKey(provider.ExportParameters(true));
      }
      Key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("MYSUPERSECRETKEY"));

      SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature);
      //SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
    }
  }
}