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
    public class EntradaSaidaRepository
    {
        public void CreateEntrada(EntradaSaida entrada)
        {
            var sql = @"
                INSERT INTO ENTRADASAIDA(
                    IDENTRADASAIDA,
                    IDMERCADORIA,
                    NOMEMERCADORIA,
                    DATAENTRADA,
                    ISSAIDA,
                    DATASAIDA)
                VALUES(
                    @IdEntradaSaida,
                    @IdMercadoria,
                    @NomeMercadoria,
                    @DataEntrada,
                    '0',
                    NULL)
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString()))
            {
                connection.Execute(sql, entrada);
            }
        }

        
        public void UpdateForSaida(EntradaSaida saida)
        {
            var sql = @"
                UPDATE ENTRADASAIDA
                SET
                    ISSAIDA = 1,
                    DATASAIDA = GETDATE()
                WHERE
                    IDENTRADASAIDA = @IdEntradaSaida
            ";

            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString()))
            {
                connection.Execute(sql, saida);
            }
        }


        public List<EntradaSaida> GetDateEntrada(DateTime dataInicio, DateTime dataFim)
        {
            var sql = @"
               SELECT * FROM ENTRADASAIDA 
                    WHERE ISSAIDA = 0 AND
                        DATAENTRADA between @DataInicio 
                                        and @DataFim
                        ORDER BY DATAENTRADA
            ";
            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString()))
            {
                return connection.Query<EntradaSaida>(sql, new { dataInicio, dataFim }).ToList();
            }
        }

        public List<EntradaSaida> GetDateSaida(DateTime dataInicio, DateTime dataFim)
        {
            var sql = @"
               SELECT * FROM ENTRADASAIDA 
                    WHERE DATASAIDA between @DataInicio 
                                        and @DataFim
                    ORDER BY DATASAIDA
            ";
            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString()))
            {
                return connection.Query<EntradaSaida>(sql, new { dataInicio, dataFim }).ToList();
            }
        }

        public List<EntradaSaida> GetDateForDashboard(DateTime dataInicio, DateTime dataFim)
        {
            var sql = @"
               SELECT * FROM ENTRADASAIDA 
                    WHERE DATAENTRADA between @DataInicio 
                                            and @DataFim
            ";
            using (var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString()))
            {
                return connection.Query<EntradaSaida>(sql, new { dataInicio, dataFim }).ToList();
            }
        }

        public byte[] GerarRelatorioExcel(List<EntradaSaida> entradaSaida)
        {
            var reportRepository = new ReportRepository();
            if (entradaSaida == null || entradaSaida.Count == 0)
            {
                throw new Exception("Não há dados para geração do relatório excel.");
            }
            return reportRepository.CreateExcel(entradaSaida);
        }

        public byte[] GerarRelatorioPdf(List<EntradaSaida> entradaSaida)
        {
            var reportRepository = new ReportRepository();
            if (entradaSaida == null || entradaSaida.Count == 0)
            {
                throw new Exception("Não há dados para geração do relatório pdf.");

            }

            return reportRepository.CreatePdf(entradaSaida);
        }

    }
}
