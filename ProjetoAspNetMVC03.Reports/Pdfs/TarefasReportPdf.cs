using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using ProjetoAspNetMVC03.Reports.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAspNetMVC03.Reports.Pdfs
{
    public class TarefasReportPdf
    {
        //método para desenhar e retornar o conteudo do relatorio..
        public byte[] GerarRelatorio(RelatorioTarefasData data)
        {
            var memoryStream = new MemoryStream();
            var pdf = new PdfDocument(new PdfWriter(memoryStream));

            using (var document = new Document(pdf))
            {
                //logotipo
                var img = ImageDataFactory.Create("https://www.cotiinformatica.com.br/imagens/logo-coti-informatica.png");
                document.Add(new Image(img));

                document.Add(new Paragraph("\n"));

                //título do relatorio
                document.Add(new Paragraph("Relatório de Tarefas").SetFontSize(24));

                //dados do usuario
                document.Add(new Paragraph("Dados do Usuário:"));
                document.Add(new Paragraph($"Nome: {data.Usuario.Nome}"));
                document.Add(new Paragraph($"Email: {data.Usuario.Email}"));
                document.Add(new Paragraph("\n"));

                //periodo do relatorio
                document.Add(new Paragraph("Período:"));
                document.Add(new Paragraph($"Data de início: {data.DataInicio.ToString("ddd, dd/MM/yyyy")}"));
                document.Add(new Paragraph($"Data de Término: {data.DataTermino.ToString("ddd, dd/MM/yyyy")}"));
                document.Add(new Paragraph("\n"));

                //tabela contendo as tarefas
                var table = new Table(5); //5 -> quantidade de colunas

                //cabeçalho da tabela
                table.AddHeaderCell("Nome da tarefa");
                table.AddHeaderCell("Data");
                table.AddHeaderCell("Hora");
                table.AddHeaderCell("Descrição");
                table.AddHeaderCell("Prioridade");

                //imprimir as tarefas obtidas
                foreach (var item in data.Tarefas)
                {
                    table.AddCell(item.Nome);
                    table.AddCell(item.Data.ToString("dd/MM/yyyy"));
                    table.AddCell(item.Hora.ToString(@"hh\:mm"));
                    table.AddCell(item.Descricao);
                    table.AddCell(item.Prioridade);
                }

                document.Add(table);

                document.Add(new Paragraph("\n"));
                document.Add(new Paragraph($"Quantidade de tarefas: {data.Tarefas.Count()}"));
            }

            return memoryStream.ToArray();
        }
    }
}


