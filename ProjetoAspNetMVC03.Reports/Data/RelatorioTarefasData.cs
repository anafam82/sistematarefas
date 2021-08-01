using ProjetoAspNetMVC03.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAspNetMVC03.Reports.Data
{
    public class RelatorioTarefasData
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }

        public Usuario Usuario { get; set; }
        public List<Tarefa> Tarefas { get; set; }
    }
}
