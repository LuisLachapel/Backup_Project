using Services.Users;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ClosedXML.Excel;


namespace Services.Files
{
    public class FileService : IFileService
    {
        private readonly IUserService _userService;

        public FileService(IUserService userService)
        {
            _userService = userService;
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public byte[] ExcelTable(DateTime? startDate, DateTime? endDate)
        {
            var users = _userService.GetUserNotesSummaries(startDate, endDate);
            if (users == null || !users.Any())
            {
                throw new ArgumentException("No hay una lista de usuarios");
            }
            using(var book = new XLWorkbook())
            {
                var sheet = book.Worksheets.Add("Reporte de usuarios");

                var tableData = users.Select(u => new
                {
                    id = u.id,
                    nombre = u.name,
                    cargo = u.position,
                    registros = u.records,
                    status = u.status,
                });

                var table = sheet.Cell(1,1).InsertTable(tableData, "Usuarios");
                table.ShowAutoFilter = true;
                table.ShowTotalsRow = true;
                table.Field("registros").TotalsRowFunction = XLTotalsRowFunction.Sum;

                sheet.ColumnsUsed().AdjustToContents();

                
                /*
                // Encabezado
                sheet.Cell(1, 1).Value = "Id";
                sheet.Cell(1, 2).Value = "Nombre";
                sheet.Cell(1, 3).Value = "Cargo";
                sheet.Cell(1, 4).Value = "Registros";
                sheet.Cell(1, 5).Value = "Status";

                int row = 2;
                foreach(var user in users)
                {
                    sheet.Cell(row,1).Value = user.id;
                    sheet.Cell(row,2).Value = user.name;
                    sheet.Cell(row,3).Value = user.position;
                    sheet.Cell(row,4).Value = user.records;
                    sheet.Cell(row,5).Value = user.status;
                    row++;

                }

                sheet.ColumnsUsed().AdjustToContents();

                */

                using (var stream = new MemoryStream())
                {
                    book.SaveAs(stream);
                    return stream.ToArray();
                }


            }
        }

        public byte[] UserReport(DateTime? startDate, DateTime? endDate)
        {
            
            var users = _userService.GetUserNotesSummaries(startDate, endDate);
            if(users == null || !users.Any())
            {
                throw new ArgumentException("No hay una lista de usuarios");
            }

            var totalRecords = users.Sum(u => u.records);

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    // Header
                    page.Header().Text("Listado de Usuarios").Bold().FontSize(18).AlignCenter();

                    //Table
                    page.Content().Column(column =>
                    {
                        column.Item().Table(table =>
                        {
                            //Definición de columnas
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(2);   //id
                                columns.RelativeColumn(3);   // Nombre
                                columns.RelativeColumn(2);   // Cargo
                                columns.RelativeColumn(2);   // Registro
                                columns.RelativeColumn(2);   // Status
                            });

                            //Cabeceras
                            table.Header(header =>
                            {
                                header.Cell().Background("#257272").Text("Id").FontColor("#fff").Bold();
                                header.Cell().Background("#257272").Text("Nombre").FontColor("#fff").Bold();
                                header.Cell().Background("#257272").Text("Cargo").FontColor("#fff").Bold();
                                header.Cell().Background("#257272").Text("Registros").FontColor("#fff").Bold();
                                header.Cell().Background("#257272").Text("Status").FontColor("#fff").Bold();
                            });

                            foreach (var user in users)
                            {
                                table.Cell().BorderBottom(0.5f).Text(user.id.ToString());
                                table.Cell().BorderBottom(0.5f).Text(user.name);
                                table.Cell().BorderBottom(0.5f).Text(user.position);
                                table.Cell().BorderBottom(0.5f).Text(user.records.ToString());
                                table.Cell().BorderBottom(0.5f).Text(user.status);
                            }
                        });

                       
                        column.Item().PaddingTop(10).AlignRight().Text($"Total de registros: {totalRecords}")
                              .Bold().FontSize(12);
                    });
                    
                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.Span("Página ");
                        x.CurrentPageNumber();
                        x.Span(" de ");
                        x.TotalPages();
                    });
                });
            });

            return document.GeneratePdf();

        }
    }
}
