using System;

namespace Api.Domain.Dtos
{
  public class ResponseDtoToken
  {
    public bool Authenticated { get; set; }
    public string AccessToken { get; set; }
    public DateTime Created { get; set; }
    public DateTime Expiration { get; set; }
    public string UserName { get; set; }
    public string UserEmail { get; set; }
    //public UserToken<TKey> UserToken { get; set; }
  }
}