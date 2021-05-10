using System;
using Newtonsoft.Json;

namespace Api.Integration.Test
{
  public class LoginResponseDto
  {
    [JsonProperty("success")]
    public bool success { get; set; }

    [JsonProperty("message")]
    public string message { get; set; }

    [JsonProperty("data")]
    public TokenResponseDto data { get; set; }

    public class TokenResponseDto
    {
      [JsonProperty("authenticated")]
      public bool authenticated { get; set; }

      [JsonProperty("accessToken")]
      public string accessToken { get; set; }

      [JsonProperty("created")]
      public DateTime created { get; set; }

      [JsonProperty("expiration")]
      public DateTime expiration { get; set; }

      [JsonProperty("userEmail")]
      public string userEmail { get; set; }

      [JsonProperty("userName")]
      public string userName { get; set; }
    }

  }
}