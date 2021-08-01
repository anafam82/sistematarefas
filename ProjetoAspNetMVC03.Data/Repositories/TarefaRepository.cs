using Dapper;
using ProjetoAspNetMVC03.Data.Entities;
using ProjetoAspNetMVC03.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAspNetMVC03.Data.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        //atributo
        private readonly string _connectionstring;

        //construtor com entrada de argumentos (inicialização)
        public TarefaRepository(string connectionstring)
        {
            _connectionstring = connectionstring;
        }

        public void Inserir(Tarefa tarefa)
        {
            var query = @"
                        INSERT INTO TAREFA(
                            IDTAREFA,
                            NOME,
                            DATA,
                            HORA,
                            DESCRICAO,
                            PRIORIDADE,
                            IDUSUARIO)      
                        VALUES(
                            NEWID(),
                            @Nome,
                            @Data,
                            @Hora,
                            @Descricao,
                            @Prioridade,
                            @IdUsuario)
                    ";

            //abrindo conexão com o banco de dados do SqlServer
            using (var connection = new SqlConnection(_connectionstring))
            {
                //executando a query (comando SQL)
                connection.Execute(query, tarefa);
            }
        }

        public void Alterar(Tarefa tarefa)
        {
            var query = @"
                        UPDATE TAREFA SET
                            NOME = @Nome,
                            DATA = @Data,
                            HORA = @Hora,
                            DESCRICAO = @Descricao,
                            PRIORIDADE = @Prioridade
                        WHERE
                            IDTAREFA = @IdTarefa
                    ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                connection.Execute(query, tarefa);
            }
        }

        public void Excluir(Tarefa tarefa)
        {
            var query = @"
                        DELETE FROM TAREFA
                        WHERE IDTAREFA = @IdTarefa
                    ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                connection.Execute(query, tarefa);
            }
        }

        public List<Tarefa> ConsultarPorUsuario(Guid idUsuario)
        {
            var query = @"
                        SELECT * FROM TAREFA
                        WHERE IDUSUARIO = @idUsuario
                        ORDER BY DATA DESC, HORA DESC
                    ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                return connection
                        .Query<Tarefa>(query, new { idUsuario })
                        .ToList();
            }
        }

        public Tarefa ObterPorId(Guid idTarefa)
        {
            var query = @"
                        SELECT * FROM TAREFA
                        WHERE IDTAREFA = @idTarefa
                    ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                return connection
                        .Query<Tarefa>(query, new { idTarefa })
                        .FirstOrDefault();
            }
        }

        public List<Tarefa> ConsultarPorUsuarioEPeriodo(Guid idUsuario, DateTime dataInicio, DateTime dataTermino)
        {
            var query = @"
                        SELECT * FROM TAREFA
                        WHERE IDUSUARIO = @idUsuario
                        AND DATA BETWEEN @dataInicio AND @dataTermino
                        ORDER BY DATA DESC, HORA DESC
                    ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                return connection
                    .Query<Tarefa>(query, new { idUsuario, dataInicio, dataTermino })
                    .ToList();
            }
        }
    }
}