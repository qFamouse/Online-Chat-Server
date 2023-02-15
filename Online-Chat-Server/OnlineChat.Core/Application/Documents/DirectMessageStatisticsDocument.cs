using Data.Functions;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Application.Documents;

public class DirectMessageStatisticsDocument : IDocument
{
    public DirectMessageStatistics Model { get; }

    public DirectMessageStatisticsDocument(DirectMessageStatistics model)
    {
        Model = model;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container
            .Page(page =>
            {
                page.Margin(50);

                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeTable);
                page.Footer().Element(ComposeFooter);
            });
    }

    private void ComposeHeader(IContainer container)
    {
        container
            .Height(50)
            .Background(Colors.Red.Darken1)
            .AlignCenter()
            .Text("Your message statistics")
            .FontSize(20)
            .FontColor(Colors.White);
    }

    private void ComposeTable(IContainer container)
    {
        container.Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.ConstantColumn(25);
                columns.RelativeColumn();
                columns.RelativeColumn();
            });

            table.Header(header =>
            {
                header.Cell().Element(CellStyle).Text("#");
                header.Cell().Element(CellStyle).Text("Description");
                header.Cell().Element(CellStyle).AlignRight().Text("Value");

                static IContainer CellStyle(IContainer container)
                {
                    return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                }
            });

            // 1
            table.Cell().Element(CellStyle).Text("1");
            table.Cell().Element(CellStyle).Text("Total messages for all time");
            table.Cell().Element(CellStyle).AlignRight().Text(Model.TotalMessages);

            // 2
            table.Cell().Element(CellStyle).Text("2");
            table.Cell().Element(CellStyle).Text("Total messages sent for all time");
            table.Cell().Element(CellStyle).AlignRight().Text(Model.TotalSent);

            // 3
            table.Cell().Element(CellStyle).Text("3");
            table.Cell().Element(CellStyle).Text("Total messages received for all time");
            table.Cell().Element(CellStyle).AlignRight().Text(Model.TotalReceived);

            static IContainer CellStyle(IContainer container)
            {
                return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
            }
        });
    }

    private void ComposeFooter(IContainer container)
    {
        container
            .Height(50)
            .AlignCenter()
            .AlignBottom()
            .Text("Copyright © 2023 Online Chat");
    }
}