using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAspNetMVC03.Models
{
    public class UsuarioEditarSenhaModel
    {
        [Required(ErrorMessage = "Por favor, informe a sua senha atual.")]
        public string SenhaAtual { get; set; }

        [StrongPassword(ErrorMessage = "Informe pelo menos 1 letra maiúscula, 1 letra minúscula, 1 número e 1 caractere especial (@ # $ % & !).")]
        [MinLength(8, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(20, ErrorMessage = "Por favor informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe a sua nova senha")]
        public string NovaSenha { get; set; }

        [Compare("NovaSenha", ErrorMessage = "Senhas não conferem.")]
        [Required(ErrorMessage = "Por favor, confirme a sua nova senha")]
        public string NovaSenhaConfirmacao { get; set; }
    }
}

