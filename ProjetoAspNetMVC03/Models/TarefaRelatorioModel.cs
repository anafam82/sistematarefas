using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAspNetMVC03.Models
{
    public class TarefaRelatorioModel
    {
        [Required(ErrorMessage = "Por favor, selecione a data de inicio.")]
        public string DataInicio { get; set; }

        [Required(ErrorMessage = "Por favor, selecione a data de término.")]
        public string DataTermino { get; set; }
    }
}