using Dapper;
using MStarSupply.Data.Configurations;
using MStarSupply.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MStarSupply.Data.Repositories
{
    public class UsuarioRepository
    {
        public Usuario GetByEmailAndSenha(string email, string senha)
        {
            var sql = @"
                        SELECT * FROM USUARIO
                        WHERE EMAIL = @email
                        AND SENHA = @senha
                        ";

            using (var connection = new SqlConnection
            (SqlServerConfiguration.GetConnectionString()))

            {
                return connection.Query<Usuario>(sql, new { email, senha }).FirstOrDefault();
            }
        }
    }
}
