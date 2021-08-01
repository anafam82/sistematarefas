using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAspNetMVC03.Models
{
    public class AccountLoginModel
    {
        [EmailAddress(ErrorMessage = "Por favor, entre com um endereço valido.")]
        [Required(ErrorMessage = "Por favor, informe seu email.")]
        public string Email { get; set; }
        
        [MinLength(8, ErrorMessage = "Informe no mínimo {1} caracteres.")]
        
        [MaxLength(20, ErrorMessage ="Informe no maximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe sua senha.")]
        public string Senha { get; set; }
    }
}
