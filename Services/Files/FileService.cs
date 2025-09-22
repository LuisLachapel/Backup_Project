using Services.Users;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ClosedXML.Excel;
using Services.Notes;
using Services.Notes.Models;


namespace Services.Files
{
    public class FileService : IFileService
    {
        private readonly IUserService _userService;
        private readonly INoteService _noteService;

        
        public FileService(IUserService userService,INoteService noteService)
        {
            _userService = userService;
            _noteService = noteService;
            QuestPDF.Settings.License = LicenseType.Community;
        }

        //Usuarios
        public byte[] ExcelUserReport(DateTime? startDate, DateTime? endDate)
        {
            var users = _userService.GetUserNotesSummaries(startDate, endDate);
            if (users == null || !users.Any())
            {
                throw new ArgumentException("No hay una lista de usuarios");
            }
            using(var book = new XLWorkbook())
            {
                var sheet = book.Worksheets.Add("Reporte de usuarios");


                var rangeDate = startDate.HasValue && endDate.HasValue ? $"Periodo: {startDate.Value:dd/M/yyyy} - {endDate.Value:dd/M/yyyy}" :
                startDate.HasValue ? $"Periodo desde: {startDate.Value:dd/M/yyyy} hasta {DateTime.Now:dd/M/yyyy}" :
                endDate.HasValue ? $" Registros hasta: {endDate.Value.ToString("dd/M/yyyy")}" : "";

                sheet.Cell(1, 1).Value = "Backup System";
                sheet.Cell(1, 1).Style.Font.Bold = true;
                sheet.Cell(1, 1).Style.Font.FontSize = 18;

                sheet.Cell(2, 1).Value = $"Reporte del {DateTime.Now:dd/MM/yyyy}";
                sheet.Cell(2, 1).Style.Font.Italic = true;

                if (!string.IsNullOrEmpty(rangeDate))
                {
                    sheet.Cell(3, 1).Value = rangeDate;
                    sheet.Cell(3, 1).Style.Font.Italic = true;
                    sheet.Cell(3, 1).Style.Font.FontSize = 11;
                }


                var tableData = users.Select(u => new
                {
                    u.id,
                    nombre = u.name,
                    cargo = u.position,
                    registros = u.records,
                    u.status,
                });

                var table = sheet.Cell(5,1).InsertTable(tableData, "Usuarios");
                table.ShowAutoFilter = true;
                table.ShowTotalsRow = true;
                //table.Field("registros").TotalsRowFunction = XLTotalsRowFunction.Sum;

                sheet.ColumnsUsed().AdjustToContents();

                //Diseño de tabla
                var header = table.HeadersRow();
                header.Style.Fill.BackgroundColor = XLColor.FromHtml("#257272");
                header.Style.Font.FontColor = XLColor.White;

                int rowIndex = 0;

                foreach(var row in table.DataRange.Rows())
                {
                    if(rowIndex % 2 == 0)
                    {
                        row.Style.Fill.BackgroundColor = XLColor.FromHtml("#339E9E"); 
                    }
                    else
                    {
                        row.Style.Fill.BackgroundColor = XLColor.FromHtml("#3EC1C1");
                    }
                    row.Style.Font.FontColor = XLColor.White;
                    row.Style.Font.Bold = true;
                    rowIndex++;
                }

                int lastRow = table.LastRow().RowNumber();
                var totalRecords = users.Sum(u => u.records);
                sheet.Cell(lastRow + 1, 1).Value = $"Total de registros: {totalRecords}";
                sheet.Cell(lastRow + 1, 1).Style.Font.Bold = true;
                sheet.Cell(lastRow + 1, 1).Style.Font.FontSize = 12;



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

            var rangeDate = startDate.HasValue && endDate.HasValue ? $"Periodo: {startDate.Value:dd/M/yyyy} - {endDate.Value:dd/M/yyyy}" :
                startDate.HasValue ? $"Periodo desde: {startDate.Value:dd/M/yyyy} hasta {DateTime.Now:dd/M/yyyy}" :
                endDate.HasValue ? $" Registros hasta: {endDate.Value.ToString("dd/M/yyyy")}" : "";



            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    // Header
                    page.Header().Row(row =>
                    {
                        row.ConstantItem(140).Height(60).Placeholder();
                        row.RelativeItem().PaddingTop(15).PaddingLeft(30).Column(column =>
                        {
                            column.Item().AlignCenter().Text("Backup System").Bold().FontSize(22);
                        });

                        row.ConstantItem(200).AlignRight().Column(column =>
                        {
                            column.Item().Text($"Reporte del {DateTime.Now:dd/MM/yyyy}").Italic().FontSize(11).AlignRight();
                        });


                    });

                    //Table
                    page.Content().PaddingTop(20).Column(column =>
                    {
                        column.Item().Row(row => {
                            row.RelativeItem().Column(columnHeader =>
                            {
                                columnHeader.Item().Text("Listado de usuarios").FontSize(15).Bold();
                            });
                        
                        });
                        // Línea con la fecha de generación del reporte
                        if (!string.IsNullOrEmpty(rangeDate))
                        {
                            column.Item().Row(row => {
                                row.RelativeItem().Column(columnHeader => {
                                    columnHeader.Item().Text(rangeDate).FontSize(10);

                                });

                            });
                        }


                        column.Item().PaddingVertical(10);
                        
                        column.Item().Table(table =>
                        {
                            
                            //Definición de columnas
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(40);   //id
                                columns.RelativeColumn();   // Nombre
                                columns.RelativeColumn();   // Cargo
                                columns.ConstantColumn(65);   // Registro
                                columns.ConstantColumn(50);   // Status
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

                            int rowIndex = 0;
                            foreach (var user in users)
                            {
                                var backgroundColor = rowIndex % 2 == 0 ? "#F5F5F5" : "#FFFFFF"; // Gris claro para pares

                                table.Cell().Background(backgroundColor).Text(user.id.ToString());
                                table.Cell().Background(backgroundColor).Text(user.name);
                                table.Cell().Background(backgroundColor).Text(user.position);
                                table.Cell().Background(backgroundColor).Text(user.records.ToString());
                                table.Cell().Background(backgroundColor).Text(user.status);

                                rowIndex++;
                            }
                            //table.Cell().ColumnSpan(5).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                            //table.Cell().ColumnSpan(4).AlignRight().Text($"Total de registros:").Bold();
                            //table.Cell().AlignRight().Text(totalRecords.ToString());
                        });


                        column.Item().PaddingTop(10).AlignRight().Text($"Total de registros: {totalRecords}")
                              .Bold().FontSize(12);
                    });
                    
                    page.Footer().AlignRight().Text(x =>
                    {
                        x.Span("Página ").FontSize(10);
                        x.CurrentPageNumber().FontSize(10);
                        x.Span(" de ").FontSize(10);
                        x.TotalPages().FontSize(10);
                    });
                });
            });

            return document.GeneratePdf();

        }


        //Notas
        public byte[] ExcelNoteReport(DateTime? startDate, DateTime? endDate)
        {
            var notes = _noteService.GetNotesByUsers(startDate, endDate);
            if(notes == null || !notes.Any())
            {
                throw new ArgumentException("No hay notas disponibles");

            }

            int totalRecords = notes.Count();

            var groupedUser = notes.GroupBy(n => new { n.id, n.username, n.position }).Select(group => new UserNoteModel
            {
                id = group.Key.id,
                username = group.Key.username,
                position = group.Key.position,
                notes = group.Select(n => new UserNoteModel.Note
                {
                    title = n.title,
                    description = n.description,
                    date = n.date,
                }).ToList()
            }).ToList();

            var rangeDate = startDate.HasValue && endDate.HasValue ? $"Periodo: {startDate.Value:dd/M/yyyy} - {endDate.Value:dd/M/yyyy}" :
                startDate.HasValue ? $"Periodo desde: {startDate.Value:dd/M/yyyy} hasta {DateTime.Now:dd/M/yyyy}" :
                endDate.HasValue ? $" Registros hasta: {endDate.Value.ToString("dd/M/yyyy")}" : "";

            using (var book = new XLWorkbook())
            {
                var sheet = book.Worksheets.Add("Notas");

                // Encabezado
                sheet.Cell(1, 1).Value = "Backup System";
                sheet.Cell(1, 1).Style.Font.Bold = true;
                sheet.Cell(1, 1).Style.Font.FontSize = 18;

                sheet.Cell(2, 1).Value = $"Reporte del {DateTime.Now:dd/MM/yyyy}";
                sheet.Cell(2, 1).Style.Font.Italic = true;

                if (!string.IsNullOrEmpty(rangeDate))
                {
                    sheet.Cell(3, 1).Value = rangeDate;
                    sheet.Cell(3, 1).Style.Font.Italic = true;
                    sheet.Cell(3, 1).Style.Font.FontSize = 11;
                }

                int currentRow = 5;

                foreach (var user in groupedUser)
                {
                    // Nombre de usuario
                    sheet.Cell(currentRow, 1).Value = $"{user.username} ({user.position})";
                    sheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    sheet.Cell(currentRow, 1).Style.Font.FontSize = 14;
                    currentRow += 2;

                    // Cabecera de la tabla
                    sheet.Cell(currentRow, 1).Value = "Título";
                    sheet.Cell(currentRow, 2).Value = "Descripción";
                    sheet.Cell(currentRow, 3).Value = "Fecha";

                    var headerRange = sheet.Range(currentRow, 1, currentRow, 3);
                    headerRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#257272");
                    headerRange.Style.Font.FontColor = XLColor.White;
                    headerRange.Style.Font.Bold = true;

                    headerRange.SetAutoFilter();

                    currentRow++;

                    // Filas de notas
                    int rowIndex = 0;
                    foreach (var note in user.notes)
                    {
                        sheet.Cell(currentRow, 1).Value = note.title;
                        sheet.Cell(currentRow, 2).Value = note.description ?? "";
                        sheet.Cell(currentRow, 3).Value = note.date.ToString("dd/MM/yyyy");

                        var rowRange = sheet.Range(currentRow, 1, currentRow, 3);
                        rowRange.Style.Fill.BackgroundColor = rowIndex % 2 == 0
                            ? XLColor.FromHtml("#F5F5F5")
                            : XLColor.White;

                        currentRow++;
                        rowIndex++;
                    }



                    // Total de registros por usuario
                    sheet.Cell(currentRow, 1).Value = $"Total de registros de {user.username}: {user.notes.Count}";
                    sheet.Cell(currentRow, 1).Style.Font.Italic = true;
                    currentRow += 3;
                }

                // Total global
                sheet.Cell(currentRow, 1).Value = $"Total de registros: {totalRecords}";
                sheet.Cell(currentRow, 1).Style.Font.Bold = true;
                sheet.Cell(currentRow, 1).Style.Font.FontSize = 12;

                // Ajustar tamaños
                sheet.ColumnsUsed().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    book.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }

        public byte[] NoteReport(DateTime? startDate, DateTime? endDate)
        {
            var notes = _noteService.GetNotesByUsers(startDate, endDate);
            if (notes == null || !notes.Any())
            {
                throw new ArgumentException("No hay una lista de usuarios");
            }

            var groupedUser = notes.GroupBy(n => new { n.id, n.username, n.position }).Select(group => new UserNoteModel
            {
                id = group.Key.id,
                username = group.Key.username,
                position = group.Key.position,
                notes = group.Select(n => new UserNoteModel.Note
                {
                    title = n.title,
                    description = n.description,
                    date = n.date,
                }).ToList()
            }).ToList();

            int totalRecords = notes.Count;

            var rangeDate = startDate.HasValue && endDate.HasValue ? $"Periodo: {startDate.Value:dd/M/yyyy} - {endDate.Value:dd/M/yyyy}" : 
                startDate.HasValue? $"Periodo desde: {startDate.Value:dd/M/yyyy} hasta {DateTime.Now:dd/M/yyyy}" :
                endDate.HasValue ? $" Registros hasta: {endDate.Value.ToString("dd/M/yyyy")}" : "";

            var document = Document.Create(container =>
            {

                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);

                    //Header
                    page.Header().Row(row => {
                        row.ConstantItem(140).Height(60).Placeholder();
                        row.RelativeItem().PaddingTop(15).PaddingLeft(30).Column(column =>
                        {
                            column.Item().AlignCenter().Text("Backup System").Bold().FontSize(22);
                        });

                        row.ConstantItem(200).AlignRight().Column(column =>
                        {
                            column.Item().Text($"Reporte del {DateTime.Now:dd/MM/yyyy}").Italic().FontSize(11).AlignRight();
                        });

                    });


                    //Contenido
                    page.Content().PaddingTop(20).Column(column =>
                    {
                        column.Item().Row(row => {
                            row.RelativeItem().Column(columnHeader =>
                            {
                                columnHeader.Item().Text("Listado de notas").FontSize(15).Bold();
                            });

                        });

                        if (!string.IsNullOrEmpty(rangeDate))
                        {
                            column.Item().Row(row => {
                                row.RelativeItem().Column(columnHeader => {
                                    columnHeader.Item().Text(rangeDate).FontSize(10);
                                
                                });
                                
                            });
                        }
                        

                        column.Item().PaddingVertical(10);

                        foreach (var user in groupedUser)
                        {
                            // Usuario header
                            column.Item().Text($"{user.username} ({user.position})")
                                .SemiBold()
                                .FontSize(13)
                                .Underline();

                            // Tabla de notas de este usuario
                            column.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(130); // titulo
                                    columns.ConstantColumn(300); // descripción
                                    columns.ConstantColumn(100);  // fecha
                                });

                                table.Header(header =>
                                {
                                    header.Cell().Background("#257272").Text("Título").FontColor("#fff").Bold();
                                    header.Cell().Background("#257272").Text("Descripción").FontColor("#fff").Bold();
                                    header.Cell().Background("#257272").Text("Fecha").FontColor("#fff").Bold();
                                });

                                int rowIndex = 0;
                                foreach (var note in user.notes)
                                {
                                    var backgroundColor = rowIndex % 2 == 0 ? "#F5F5F5" : "#FFFFFF";
                                    table.Cell().Background(backgroundColor).Text(note.title);
                                    table.Cell().Background(backgroundColor).Text(note.description ?? "");
                                    table.Cell().Background(backgroundColor).Text(note.date.ToString("dd/MM/yyyy"));
                                    rowIndex++;
                                }
                            });

                            column.Item().PaddingTop(5).AlignRight().Text($"Total de registros de {user.username}: {user.notes.Count}").FontSize(11).Italic();

                            // separador entre usuarios
                            column.Item().PaddingVertical(15).LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
                        }

                        column.Item().PaddingTop(10).AlignRight().Text($"Total de registros: {totalRecords}")
                              .Bold().FontSize(12);
                    });


                    page.Footer().AlignRight().Text(footer => {
                        footer.Span("Pagina ").FontSize(10);
                        footer.CurrentPageNumber().FontSize(10);
                        footer.Span(" de ").FontSize(10);
                        footer.TotalPages().FontSize(10);

                    
                    });
                });
            });
            return document.GeneratePdf();
        }
    }
}
