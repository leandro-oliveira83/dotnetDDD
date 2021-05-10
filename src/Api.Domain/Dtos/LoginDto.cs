using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos
{
  public class LoginDto
  {
    [Required(ErrorMessage = "O e-mail é um campo obrigatório para Login.")]
    [EmailAddress(ErrorMessage = "O e-mail informado possui um formato inválido.")]
    [StringLength(100, ErrorMessage = "O e-mail deve ter no máximo {1} caracteres.")]
    public string Email { get; set; }
  }
}