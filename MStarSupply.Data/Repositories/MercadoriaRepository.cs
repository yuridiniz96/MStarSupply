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
    public class MercadoriaRepository
    {
        public void Create(Mercadoria mercadoria)
        {
            var sql = @"
                INSERT INTO MERCADORIA(
                    IDMERCADORIA,
                    NOMEMERCADORIA,
                    NUMEROREGISTRO,
                    FABRICANTE,
                    TIPOMERCADORIA,
                    DESCRICAO,
                    DATACADASTRO)
                VALUES(
                    @IdMercadoria,
                    @NomeMercadoria,
                    @NumeroRegistro,
                    @Fabricante,
                    @TipoMercadoria,
                    @Descricao,
                    @DataCadastro)
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString()))
            {
                connection.Execute(sql, mercadoria);
            }
        }

        public void Update(Mercadoria mercadoria)
        {
            var sql = @"
                UPDATE MERCADORIA
                SET
                    NOMEMERCADORIA = @NomeMercadoria,
                    NUMEROREGISTRO = @NumeroRegistro,
                    FABRICANTE = @Fabricante,
                    TIPOMERCADORIA = @TipoMercadoria,
                    DESCRICAO = @Descricao,
                    DATACADASTRO = @DataCadastro
                WHERE
                    IDMERCADORIA = @IdMercadoria
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString()))
            {
                connection.Execute(sql, mercadoria);
            }
        }

        public void Delete(Mercadoria mercadoria)
        {
            var sql = @"
                DELETE FROM MERCADORIA
                WHERE IDMERCADORIA = @IdMercadoria
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString()))
            {
                connection.Execute(sql, mercadoria);
            }
        }

        public List<Mercadoria> GetAll()
        {
            var sql = @"
                SELECT * FROM MERCADORIA
                ORDER BY NOMEMERCADORIA
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString()))
            {
                return connection.Query<Mercadoria>(sql).ToList();
            }
        }

        public List<Mercadoria> GetAllDistinct()
        {
            var sql = @"
                SELECT DISTINCT * FROM MERCADORIA
                ORDER BY NOMEMERCADORIA
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString()))
            {
                return connection.Query<Mercadoria>(sql).ToList();
            }
        }
        public Mercadoria GetById(Guid idMercadoria)
        {
            var sql = @"
                SELECT * FROM MERCADORIA
                WHERE IDMERCADORIA = @IdMercadoria
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString()))
            {
                return connection.Query<Mercadoria>(sql, new { idMercadoria }).FirstOrDefault();
            }
        }

        public Mercadoria GetNameById(Guid idMercadoria)
        {
            var sql = @"
                SELECT NOMEMERCADORIA FROM MERCADORIA
                WHERE IDMERCADORIA = @IdMercadoria
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString()))
            {
                return connection.Query<Mercadoria>(sql, new { idMercadoria }).FirstOrDefault();
            }
        }

    }
}
