using PrototipoSistema;
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
    public string Rua { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
    public string UF { get; set; }
    public string CEP { get; set; }
    public string Placa { get; set; }
    public string Marca { get; set; }
    public string Modelo { get; set; }
    public string Cor { get; set; }
    public string Ano { get; set; }
    public string Km { get; set; }
    public string Observacao { get; set; }
    public DateTime DtCadastro { get; set; }
    public DateTime DtSaida { get; set; }
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

            page.Header().Column(header =>
            {
                header.Item().Row(row =>
                {
                    // Esquerda - Dados da empresa
                    row.RelativeColumn().Column(col =>
                    {
                        col.Item().Text("JC MOTORSPORT").Bold().FontSize(14).AlignLeft();
                        col.Item().Text("End.: AV. LUIZ GONZAGA MARTINS GUIMARÃES, 164 - JD. CAMPOS ELÍSEOS").FontSize(9);
                        col.Item().Text("JUNDIAÍ - SP | CEP: 13209770 | CNPJ: 08.481.0150001-20").FontSize(9);
                        col.Item().Text("Fone: (11) 2709-5420 | Email: JCMOTORS2020@GMAIL.COM").FontSize(9);
                    });

                    // Direita - Dados da O.S.
                    row.ConstantColumn(110).Column(col =>
                    {
                        col.Item().Row(r =>
                        {
                            r.RelativeColumn().Text("O.S.:").SemiBold().FontSize(9);
                            r.ConstantColumn(60).Text(static_class.controle_os).Bold().FontSize(9);
                        });
                        col.Item().Row(r =>
                        {
                            r.RelativeColumn().Text("Data:").SemiBold().FontSize(9);
                            r.ConstantColumn(60).Text(DateTime.Now.ToString("dd/MM/yyyy")).FontSize(9);
                        });
                        col.Item().Row(r =>
                        {
                            r.RelativeColumn().Text("Hora:").SemiBold().FontSize(9);
                            r.ConstantColumn(60).Text(DateTime.Now.ToString("hh:mm")).FontSize(9);
                        });
                        col.Item().Row(r =>
                        {
                            r.RelativeColumn().Text("Pág.:").SemiBold().FontSize(9);
                            r.ConstantColumn(60).Text("1").FontSize(9);
                        });
                    });
                });
            });


            page.Content().Column(col =>
            {
                col.Spacing(10);

                // Dados do Cliente e Veículo
                col.Item().PaddingTop(10).Row(row =>
                {
                    // Coluna Esquerda - Cliente
                    row.RelativeColumn().Column(c =>
                    {
                        c.Item().Text("Dados do Cliente:").Bold();
                        c.Item().Text(txt =>
                        {
                            txt.Span("Nome: ").Italic().FontSize(9);
                            txt.Span(Cliente).NormalWeight().FontSize(9);
                        });
                        c.Item().Text(txt =>
                        {
                            txt.Span("Endereço: ").Italic().FontSize(9);
                            txt.Span(Rua).NormalWeight().FontSize(9); // Substituir se desejar
                        });
                        c.Item().Text(txt =>
                        {
                            txt.Span("Cidade: ").Italic().FontSize(9);
                            txt.Span(Cidade).NormalWeight().FontSize(9); // Substituir se desejar
                        });
                    });

                    // Coluna Direita - UF e CEP
                    row.ConstantColumn(280).Column(c =>
                    {
                        c.Item().Text("");
                        c.Item().Text(txt =>
                        {
                            txt.Span("Bairro: ").Italic().FontSize(9);
                            txt.Span(Bairro).NormalWeight().FontSize(9); // Substituir se desejar
                        });
                        c.Item().Text(txt =>
                        {
                            txt.Span("UF: ").Italic().FontSize(9);
                            txt.Span(UF).NormalWeight().FontSize(9); // Substituir se desejar
                        });
                        c.Item().Text(txt =>
                        {
                            txt.Span("CEP: ").Italic().FontSize(9);
                            txt.Span(CEP).NormalWeight().FontSize(9); // Substituir se desejar
                        });
                    });
                });

                col.Item().PaddingTop(10).Row(row =>
                {
                    row.RelativeColumn().Text(txt =>
                    {
                        txt.Span("Dados do Veículo:").Bold();
                    });
                });

                col.Item().Row(row =>
                {
                    row.RelativeColumn().Text(txt =>
                    {
                        txt.Span("Placa: ").Italic().FontSize(10);
                        txt.Span(Placa).NormalWeight().FontSize(10);
                        txt.Span("   Modelo: ").Italic().FontSize(10);
                        txt.Span(Modelo).NormalWeight().FontSize(10);
                        txt.Span("   Fabric.: ").Italic().FontSize(10);
                        txt.Span(Marca).NormalWeight().FontSize(10);
                        txt.Span("   Ano/Mod: ").Italic().FontSize(10);
                        txt.Span(Ano).NormalWeight().FontSize(10);
                        txt.Span("   Chassi: ").Italic().FontSize(10);
                    });
                });

                col.Item().Row(row =>
                {
                    row.RelativeColumn().Text(txt =>
                    {
                        txt.Span("Cor: ").Italic().FontSize(10);
                        txt.Span(Cor).NormalWeight().FontSize(10); // Substituir se desejar
                        txt.Span("   KM: ").Italic().FontSize(10);
                        txt.Span(Km).NormalWeight().FontSize(10);
                        txt.Span("   Dt. Ent: ").Italic().FontSize(10);
                        txt.Span(DtCadastro.ToString("dd/MM/yyyy")).NormalWeight().FontSize(10);
                        txt.Span("   Dt. Saída: ").Italic().FontSize(10);
                        txt.Span(DtSaida.ToString("dd/MM/yyyy")).NormalWeight().FontSize(10);
                    });
                });

                col.Item().PaddingTop(10).Text("Peças e Serviços").Bold().FontSize(14).AlignCenter();

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
