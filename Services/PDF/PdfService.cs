using Services.Users.Models;
using Services.Users;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;


namespace Services.PDF
{
    public class PdfService : IPdfService
    {
        private readonly IUserService _userService;

        public PdfService(IUserService userService)
        {
            _userService = userService;
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public byte[] UserReport(DateTime? startDate, DateTime? endDate)
        {
            var users = _userService.GetUserNotesSummaries(startDate, endDate);
            if(users == null || !users.Any())
            {
                throw new ArgumentException("No hay una lista de usuarios");
            }

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
                    page.Content().Table(table =>
                    {
                        //Definición de columnas
                        table.ColumnsDefinition(columns =>
                        {
                            
                            columns.RelativeColumn(3);   // Nombre
                            columns.RelativeColumn(2);   // Cargo
                            columns.RelativeColumn(2);   // Registro
                            columns.RelativeColumn(2);   // Status
                        });

                        //Cabeceras
                        table.Header(header =>
                        {
                            
                            header.Cell().Text("Nombre").Bold();
                            header.Cell().Text("Cargo").Bold();
                            header.Cell().Text("Registro").Bold();
                            header.Cell().Text("Status").Bold();
                            
                        });

                        foreach(var user in users)
                        {
                            
                            table.Cell().Text(user.name);
                            table.Cell().Text(user.position);
                            table.Cell().Text(user.records.ToString());
                            table.Cell().Text(user.status);


                        }
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
