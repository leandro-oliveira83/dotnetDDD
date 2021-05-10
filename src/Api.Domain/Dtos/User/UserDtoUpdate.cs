using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.User
{
  public class UserDtoUpdate
  {
    [Required(ErrorMessage = "O ID é um campo obrigatório")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O nome é um campo obrigatório")]
    [StringLength(60, ErrorMessage = "O nome deve ter no máximo {1} caracteres.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "O e-mail é um campo obrigatório para Login.")]
    [EmailAddress(ErrorMessage = "O e-mail informado possui um formato inválido.")]
    [StringLength(100, ErrorMessage = "O e-mail deve ter no máximo {1} caracteres.")]
    public string Email { get; set; }

    public string Sex { get; set; }

    [StringLength(10, ErrorMessage = "O telefone deve ter no máximo {1} caracteres.")]
    public string Phone { get; set; }
  }
}