using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MStarSupply.Data.Entities;

namespace MStarSuplly.Reports
{
    public class EntradaSaidaReport
    {
        public byte[] CreateExcel(List<EntradaSaida> entradasSaidas)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var excelPackage = new ExcelPackage())
            {
                var planilha = excelPackage.Workbook.Worksheets.Add("Contas");

                planilha.Cells["A1"].Value = "Relatório";
                var titulo = planilha.Cells["A1:D1"];
                titulo.Merge = true;
                titulo.Style.Font.Size = 16;
                titulo.Style.Font.Bold = true;
                titulo.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                planilha.Cells["A2"].Value = $"Gerado em:{DateTime.Now.ToString("dd/MM/yyyy")}";
                var substitulo = planilha.Cells["A2:D2"];
                substitulo.Merge = true;
                substitulo.Style.Font.Size = 14;
                substitulo.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                planilha.Cells["A4"].Value = "ID Entrada";
                planilha.Cells["B4"].Value = "ID Mercadoria";
                planilha.Cells["C4"].Value = "Mercadoria";
                planilha.Cells["D4"].Value = "Data de Entrada";
                planilha.Cells["E4"].Value = "Data de Saída";

                var cabecalhos = planilha.Cells["A4:E4"];
                cabecalhos.Style.Font.Size = 12;
                cabecalhos.Style.Font.Bold = true;
                cabecalhos.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                cabecalhos.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cabecalhos.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#363636"));

                var linha = 5;
                foreach (var item in entradasSaidas)
                {
                    planilha.Cells[$"A{linha}"].Value = item.IdEntradaSaida;
                    planilha.Cells[$"B{linha}"].Value = item.IdMercadoria;
                    planilha.Cells[$"C{linha}"].Value = item.NomeMercadoria;
                    planilha.Cells[$"D{linha}"].Value = item.DataEntrada.ToString("dd/MM/yyyy");
                    planilha.Cells[$"E{linha}"].Value = item.DataSaida.ToString("dd/MM/yyyy");

                    if (linha % 2 == 0)
                    {
                        var conteudo = planilha.Cells[$"A{linha}:E{linha}"];
                        conteudo.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        conteudo.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EEEEEE"));
                    }
                    linha++;
                }
                var dados = planilha.Cells[$"A4:E{linha - 1}"];
                dados.Style.Border.BorderAround(ExcelBorderStyle.Medium);
                planilha.Cells["A:E"].AutoFitColumns();
                return excelPackage.GetAsByteArray();
            }
        }
        public byte[] CreatePdf(List<EntradaSaida> entradasSaidas)
        {
            var memoryStream = new MemoryStream();
            var pdf = new PdfDocument(new PdfWriter(memoryStream));
            using (var document = new Document(pdf))
            {
                var logo = ImageDataFactory.Create

                ("http://www.cotiinformatica.com.br/imagens/logo-coti-informatica.png");
                document.Add(new iText.Layout.Element.Image(logo));
                document.Add(new Paragraph("Relatório de Entradas e Saídas de Mercadoria").AddStyle(new Style().SetFontSize(32)));
                document.Add(new Paragraph($"Gerado em: {DateTime.Now.ToString("dd/MM/yyyy")}").AddStyle(new Style().SetFontSize(16)));
                document.Add(new Paragraph("\n"));
                var table = new Table(4);
                table.SetWidth(UnitValue.CreatePercentValue(100));
                table.AddHeaderCell(new Paragraph("ID Entrada").AddStyle(new Style().SetFontSize(16)));
                table.AddHeaderCell(new Paragraph("ID Mercadoria").AddStyle(new Style().SetFontSize(16)));
                table.AddHeaderCell(new Paragraph("Mercadoria").AddStyle(new Style().SetFontSize(16)));
                table.AddHeaderCell(new Paragraph("Data de Entrada").AddStyle(new Style().SetFontSize(16)));
                table.AddHeaderCell(new Paragraph("Data de Saída").AddStyle(new Style().SetFontSize(16)));

                foreach (var item in entradasSaidas)
                {
                    table.AddCell(new Paragraph(item.IdEntradaSaida.ToString()).AddStyle(new Style().SetFontSize(12)));
                    table.AddCell(new Paragraph(item.IdMercadoria.ToString()).AddStyle(new Style().SetFontSize(12)));
                    table.AddCell(new Paragraph(item.NomeMercadoria.ToString()).AddStyle(new Style().SetFontSize(12)));
                    table.AddCell(new Paragraph(item.DataEntrada.ToString("dd/MM/yyyy")).AddStyle(new Style().SetFontSize(12)));
                    table.AddCell(new Paragraph(item.DataSaida.ToString("dd/MM/yyyy")).AddStyle(new Style().SetFontSize(12)));

                }
                document.Add(table);
                document.Add(new Paragraph

                ($"Quantidade: {entradasSaidas.Count}")

                .AddStyle(new Style().SetFontSize(14)));

            }
            return memoryStream.ToArray();
        }
    }
}
