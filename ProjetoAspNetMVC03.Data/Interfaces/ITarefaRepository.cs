using ProjetoAspNetMVC03.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAspNetMVC03.Data.Interfaces
{
    public interface ITarefaRepository
    {
        void Inserir(Tarefa tarefa);
        void Alterar(Tarefa tarefa);
        void Excluir(Tarefa tarefa);

        List<Tarefa> ConsultarPorUsuario(Guid idUsuario);
        List<Tarefa> ConsultarPorUsuarioEPeriodo(Guid idUsuario, DateTime dataInicio, DateTime dataTermino);
        Tarefa ObterPorId(Guid idTarefa);
    }
}

