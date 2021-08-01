using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAspNetMVC03.Models
{
    public class TarefaCadastroModel
    {
        [Required(ErrorMessage = "Por favor, informe o nome da tarefa.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data da tarefa.")]
        public string Data { get; set; }

        [Required(ErrorMessage = "Por favor, informe o hora da tarefa.")]
        public string Hora { get; set; }

        [MaxLength(150, ErrorMessage = "Por favor, informe no máximo 150 caracteres.")]
        [Required(ErrorMessage = "Por favor, informe a descrição da tarefa.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Por favor, selecione a prioridade da tarefa.")]
        public PrioridadeTarefa Prioridade { get; set; }
    }

    //utilizando para gerar um campo de opções
    public enum PrioridadeTarefa
    {
        ALTA,
        MEDIA,
        BAIXA
    }
}


