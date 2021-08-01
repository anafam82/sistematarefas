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
    public class UsuarioRepository : IUsuarioRepository
    {
        //atributo privado
        private readonly string _connectionstring;

        //método construtor para receber o valor da connectionstring
        public UsuarioRepository(string connectionstring)
        {
            _connectionstring = connectionstring;
        }

        public void Inserir(Usuario usuario)
        {
            var query = @"
                        INSERT INTO USUARIO(
                            IDUSUARIO, 
                            NOME, 
                            EMAIL, 
                            SENHA, 
                            DATACADASTRO)
                        VALUES(
                            NEWID(), 
                            @Nome, 
                            @Email, 
                            CONVERT(VARCHAR(32), HASHBYTES('MD5', @Senha), 2), 
                            GETDATE())
                    ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                connection.Execute(query, usuario);
            }
        }

        public void Alterar(Usuario usuario)
        {
            var query = @"
                    UPDATE USUARIO
                    SET
                        NOME = @Nome,
                        EMAIL = @Email,
                        CONVERT(VARCHAR(32), HASHBYTES('MD5', @Senha), 2)
                    WHERE
                        IDUSUARIO = @IdUsuario
                ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                connection.Execute(query, usuario);
            }
        }

        public void Alterar(Guid idUsuario, string novaSenha)
        {
            var query = @"
                    UPDATE USUARIO SET
                        SENHA = CONVERT(VARCHAR(32), HASHBYTES('MD5', @novaSenha), 2)
                    WHERE
                        IDUSUARIO = @idUsuario
                ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                connection.Execute(query, new { idUsuario, novaSenha });
            }
        }

        public void Excluir(Usuario usuario)
        {
            var query = @"
                        DELETE FROM USUARIO
                        WHERE IDUSUARIO = @IdUsuario
                    ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                connection.Execute(query, usuario);
            }
        }

        public List<Usuario> Consultar()
        {
            var query = @"
                        SELECT * FROM USUARIO
                        ORDER BY NOME
                    ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                return connection
                    .Query<Usuario>(query)
                    .ToList();
            }
        }

        public Usuario ObterPorId(Guid idUsuario)
        {
            var query = @"
                        SELECT * FROM USUARIO
                        WHERE IDUSUARIO = @idUsuario
                    ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                return connection
                    .Query<Usuario>(query, new { idUsuario })
                    .FirstOrDefault();
            }
        }

        public Usuario Obter(string email)
        {
            var query = @"
                        SELECT * FROM USUARIO
                        WHERE EMAIL = @email
                    ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                return connection
                    .Query<Usuario>(query, new { email })
                    .FirstOrDefault();
            }
        }

        public Usuario Obter(string email, string senha)
        {
            var query = @"
                        SELECT * FROM USUARIO
                        WHERE EMAIL = @email
                        AND SENHA = CONVERT(VARCHAR(32), HASHBYTES('MD5', @Senha), 2)
                    ";

            using (var connection = new SqlConnection(_connectionstring))
            {
                return connection
                    .Query<Usuario>(query, new { email, senha })
                    .FirstOrDefault();
            }
        }
    }
}
