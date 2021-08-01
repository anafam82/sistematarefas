using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAspNetMVC03.Models
{
    public class AccountRegisterModel
    {
        [MinLength(6, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(150, ErrorMessage = "Por favor informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe o seu nome.")]
        public string Nome { get; set; }

        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de email válido.")]
        [Required(ErrorMessage = "Por favor, informe o seu email.")]
        public string Email { get; set; }

        [StrongPassword(ErrorMessage = "Informe pelo menos 1 letra maiúscula, 1 letra minúscula, 1 número e 1 caractere especial (@ # $ % & !).")]
        [MinLength(8, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(20, ErrorMessage = "Por favor informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe a sua senha")]
        public string Senha { get; set; }

        [Compare("Senha", ErrorMessage = "Senhas não conferem.")]
        [Required(ErrorMessage = "Por favor, confirme a sua senha")]
        public string SenhaConfirmacao { get; set; }
    }

    //criando um validador customizado para o campo de senha
    public class StrongPassword : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                var senha = value.ToString();

                return senha.Any(char.IsUpper)
                    && senha.Any(char.IsLower)
                    && senha.Any(char.IsDigit)
                    && (senha.Contains("@")
                        || senha.Contains("#")
                        || senha.Contains("$")
                        || senha.Contains("%")
                        || senha.Contains("&")
                        || senha.Contains("!"));
            };

            return false;
        }
    }
}