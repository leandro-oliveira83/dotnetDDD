using System.Collections.Generic;

namespace Api.Domain.Dtos.User
{
  public class UserTokenDTO<T>
  {
    public T Id { get; set; }
    public string Email { get; set; }
    //public IEnumerable<UserClaim> Claims { get; set; }
  }
}