using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;

public class osPdf : IDocument
{
    public string Cliente { get; set; }
    public string Documento { get; set; }
    public string Telefone { get; set; }
    public string Placa { get; set; }
    public string Marca { get; set; }
    public string Modelo { get; set; }
    public string Ano { get; set; }
    public string Km { get; set; }
    public string Observacao { get; set; }
    public DateTime DtCadastro { get; set; }
    public DateTime DtSaida { get; set; }
    public bool Pago { get; set; }
    public decimal Total { get; set; }
    public decimal TotalPecas { get; set; }
    public decimal TotalServicos { get; set; }
    public List<(string Nome, string Qtd, string Valor)> Pecas { get; set; }
    public List<(string Nome, string Qtd, string Valor)> Servicos { get; set; }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public DocumentSettings GetSettings() => DocumentSettings.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Margin(30);
            page.Size(PageSizes.A4);
            page.PageColor(Colors.White);

            page.Header().Text("Ordem de Serviço").FontSize(20).Bold().AlignCenter();

            page.Content().Column(col =>
            {
                col.Spacing(10);

                col.Item().Text($"Cliente: {Cliente} | Documento: {Documento} | Telefone: {Telefone}");
                col.Item().Text($"Veículo: {Marca} {Modelo} {Ano} | KM: {Km}");
                col.Item().Text($"Placa: {Placa}").Bold();
                col.Item().Text($"Data Cadastro: {DtCadastro:dd/MM/yyyy} | Saída: {DtSaida:dd/MM/yyyy}");
                col.Item().Text($"Observações: {Observacao}");

                // Peças
                col.Item().PaddingTop(10).Text("Peças").Bold().FontSize(12);
                col.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(); // Nome
                        columns.ConstantColumn(40); // Qtd
                        columns.ConstantColumn(60); // Valor unitário
                        columns.ConstantColumn(80); // Total
                    });

                    // Cabeçalho
                    table.Header(header =>
                    {
                        header.Cell().Element(CellStyle).Text("Nome");
                        header.Cell().Element(CellStyle).Text("Qtd");
                        header.Cell().Element(CellStyle).Text("Valor");
                        header.Cell().Element(CellStyle).Text("Total");
                    });

                    // Linhas
                    foreach (var p in Pecas)
                    {
                        var qtd = decimal.TryParse(p.Qtd, out var q) ? q : 0;
                        var valor = decimal.TryParse(p.Valor, out var v) ? v : 0;
                        var total = qtd * valor;

                        table.Cell().Element(CellStyle).Text(p.Nome);
                        table.Cell().Element(CellStyle).Text(p.Qtd);
                        table.Cell().Element(CellStyle).Text(valor.ToString("C"));
                        table.Cell().Element(CellStyle).Text(total.ToString("C"));
                    }

                    // Linha de total
                    table.Cell().ColumnSpan(3).AlignRight().Padding(5).Text("Total Peças:").Bold();
                    table.Cell().Element(CellStyle).Text(TotalPecas.ToString("C")).Bold();
                });

                // Serviços
                col.Item().PaddingTop(10).Text("Serviços").Bold().FontSize(12);
                col.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(); // Nome
                        columns.ConstantColumn(40); // Qtd
                        columns.ConstantColumn(60); // Valor unitário
                        columns.ConstantColumn(80); // Total
                    });

                    // Cabeçalho
                    table.Header(header =>
                    {
                        header.Cell().Element(CellStyle).Text("Nome");
                        header.Cell().Element(CellStyle).Text("Qtd");
                        header.Cell().Element(CellStyle).Text("Valor");
                        header.Cell().Element(CellStyle).Text("Total");
                    });

                    // Linhas
                    foreach (var s in Servicos)
                    {
                        var qtd = decimal.TryParse(s.Qtd, out var q) ? q : 0;
                        var valor = decimal.TryParse(s.Valor, out var v) ? v : 0;
                        var total = qtd * valor;

                        table.Cell().Element(CellStyle).Text(s.Nome);
                        table.Cell().Element(CellStyle).Text(s.Qtd);
                        table.Cell().Element(CellStyle).Text(valor.ToString("C"));
                        table.Cell().Element(CellStyle).Text(total.ToString("C"));
                    }

                    // Linha de total
                    table.Cell().ColumnSpan(3).AlignRight().Padding(5).Text("Total Serviços:").Bold();
                    table.Cell().Element(CellStyle).Text(TotalServicos.ToString("C")).Bold();
                });

                // Totais
                col.Item().PaddingTop(10).Text($"Total Geral: {Total:C}").FontSize(12).Bold();
            });
        });
    }

    private IContainer CellStyle(IContainer container) =>
        container.Padding(4).BorderBottom(1).BorderColor(Colors.Grey.Lighten2);
}
