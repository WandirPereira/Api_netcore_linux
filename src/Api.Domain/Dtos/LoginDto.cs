using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email é um campo obrigatório!")]
        [EmailAddress(ErrorMessage = "Email em formato inválido!")]
        [StringLength(100, ErrorMessage = "O email deve ter no máximo {1} caracteres!")]
        public string Email { get; set; }
    }
}
