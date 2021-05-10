namespace Api.Domain.Entities
{
  public class UserEntity : BaseEntity
  {
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public string Sex { get; set; }
  }
}