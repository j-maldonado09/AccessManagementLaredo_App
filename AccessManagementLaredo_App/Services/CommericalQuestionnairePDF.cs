using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using AccessManagementLaredo.HelperModels;

namespace AccessManagementLaredo_App.Services
{
    public class CommericalQuestionnairePDF
    {
        public void CreatePdf(PermitRequestCompositeHelperModel permitRequestCompositeHelperModel, string physicalPath)
        {
            float fontSize = 8.5f;
            string fileName = Path.Combine(physicalPath, "Documents", "Form 2534 Commercial Questionnaire.pdf");

            var document = Document.Create(container =>
            {                
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(20);

                    page.Header().Element(header =>
                    {
                        header.Image(Path.Combine(physicalPath, "images", "Form 2534 Commercial Questionnaire Header.png"));
                    });

                    page.Content()
                        .PaddingTop(12)
                        .PaddingLeft(10)
                        .PaddingRight(10)
                        .Column(column =>
                        {                           
                            column.Item()
                                  .PaddingTop(2)
                                  .Row(row =>
                                  {
                                      row.AutoItem().Text("Date:").FontSize(fontSize);
                                      row.Spacing(4);
                                      row.ConstantItem(100).BorderBottom(0.25f).Text(" ").FontSize(fontSize);
                                  });
                            column.Item()
                                  .PaddingTop(8)
                                  .Row(row =>
                                  {
                                      row.AutoItem().Text("County:").FontSize(fontSize);
                                      row.Spacing(4);
                                      row.RelativeItem().BorderBottom(0.25f).Text(" ").FontSize(fontSize);
                                      row.Spacing(30);
                                      row.AutoItem().Text("District:").FontSize(fontSize);
                                      row.Spacing(4);
                                      row.RelativeItem().BorderBottom(0.25f).Text(" ").FontSize(fontSize);
                                  });
                            column.Item()
                                  .PaddingTop(8)
                                  .Row(row =>
                                  {
                                      row.AutoItem().Text("Highway:").FontSize(fontSize);
                                      row.Spacing(4);
                                      row.RelativeItem().BorderBottom(0.25f).Text(" ").FontSize(fontSize);
                                      row.Spacing(30);
                                      row.AutoItem().Text("Limits:").FontSize(fontSize);
                                      row.Spacing(4);
                                      row.RelativeItem().BorderBottom(0.25f).Text(" ").FontSize(fontSize);
                                  });
                            column.Item()
                                  .PaddingTop(10)
                                  .Row(row =>
                                  {
                                      row.RelativeItem().BorderBottom(2f);                                        
                                  });
                            column.Item()
                                  .PaddingTop(8)
                                  .Text(text =>
                                  {
                                      text.Span("1. Purpose of Request: ")
                                          .FontSize(fontSize)
                                          .Bold();
                                      text.Span("Explain the need for the access driveway")
                                          .FontSize(fontSize)
                                          .Italic();
                                  });
                            column.Item()
                                  .PaddingTop(10)
                                  .Row(row =>
                                  {
                                      row.RelativeItem().BorderBottom(0.25f).Text("");
                                  });
                            column.Item()
                                  .PaddingTop(8)
                                  .Text(text =>
                                  {
                                      text.Span("2. Proposed use of the driveway: ")
                                          .FontSize(fontSize)
                                          .Bold();
                                      text.Span("operations, facilities, frequency of access use")
                                          .FontSize(fontSize)
                                          .Italic();
                                  });
                            column.Item()
                                  .PaddingTop(10)
                                  .Row(row =>
                                  {
                                      row.RelativeItem().BorderBottom(0.25f).Text("");
                                  });
                            column.Item()
                                  .PaddingTop(8)
                                  .Text(text =>
                                  {
                                      text.Span("3. Background: ")
                                          .FontSize(fontSize)
                                          .Bold();
                                      text.Span("Dated chronology of previous correspondence, meetings, or discussions about driveway access")
                                          .FontSize(fontSize)
                                          .Italic();
                                  });
                            column.Item()
                                  .PaddingTop(10)
                                  .Row(row =>
                                  {
                                      row.RelativeItem().BorderBottom(0.25f).Text("");
                                  });
                            column.Item()
                                  .PaddingTop(8)
                                  .Text(text =>
                                  {
                                      text.Span("4. Participants in the request process: ")
                                          .FontSize(fontSize)
                                          .Bold();
                                      text.Span("Including city, county, developers, consultants, legal counsel, etc.")
                                          .FontSize(fontSize)
                                          .Italic();
                                  });
                            column.Item()
                                  .PaddingTop(10)
                                  .Row(row =>
                                  {
                                      row.RelativeItem().BorderBottom(0.25f).Text("");
                                  });
                            column.Item()
                                  .PaddingTop(8)
                                  .Text(text =>
                                  {
                                      text.Span("5. Funding Contributions: ")
                                          .FontSize(fontSize)
                                          .Bold();
                                  });
                            column.Item()
                                  .PaddingTop(10)
                                  .Row(row =>
                                  {
                                      row.RelativeItem().BorderBottom(0.25f).Text("");
                                  });
                            column.Item()
                                  .PaddingTop(8)
                                  .Text(text =>
                                  {
                                      text.Span("6. Highway layout showing the requested access site and upstream/downstream roadway system and other associated access: ")
                                          .FontSize(fontSize)
                                          .Bold();
                                      text.Span("Attach vicinity map (surrounding area), project location map (adjacent highway/ramps and local streets), location of access breaks (in relationship to property boundaries)")
                                          .FontSize(fontSize)
                                          .Italic();
                                  });
                            column.Item()
                                  .PaddingTop(10)
                                  .Row(row =>
                                  {
                                      row.RelativeItem().BorderBottom(0.25f).Text("");
                                  });
                            column.Item()
                                  .PaddingTop(8)
                                  .Text(text =>
                                  {
                                      text.Span("7. Existing roadway characteristics: ")
                                          .FontSize(fontSize)
                                          .Bold();
                                      text.Span("ADT (current and proposed), number of lanes, posted speed, bridge structures, utility overhead and underground (location/relocation), geometrics at proposed access (sight distance, grades, vertical/horizontal curves), pavement (structures, width and material)")
                                          .FontSize(fontSize)
                                          .Italic();
                                  });                                  
                            column.Item()
                                  .PaddingTop(10)
                                  .Row(row =>
                                  {
                                      row.RelativeItem().BorderBottom(0.25f).Text("");
                                  });
                            column.Item()
                                  .PaddingTop(8)
                                  .Text(text =>
                                  {
                                      text.Span("8. Proposed driveway: ")
                                          .FontSize(fontSize)
                                          .Bold();
                                      text.Span("Proposed radii, throat width and length, entry/exit width")
                                          .FontSize(fontSize)
                                          .Italic();
                                  });
                            column.Item()
                                  .PaddingTop(10)
                                  .Row(row =>
                                  {
                                      row.RelativeItem().BorderBottom(0.25f).Text("");
                                  });
                            column.Item()
                                  .PaddingTop(8)
                                  .Text(text =>
                                  {
                                      text.Span("9. Summary of Traffic Impact Analysis: ")
                                          .FontSize(fontSize)
                                          .Bold();
                                      text.Span("Attach copy of licensed engineer's signed and sealed TIA")
                                          .FontSize(fontSize)
                                          .Italic();
                                  });
                            column.Item()
                                  .PaddingTop(10)
                                  .Row(row =>
                                  {
                                      row.RelativeItem().BorderBottom(0.25f).Text("");
                                  });
                            column.Item()
                                  .PaddingTop(8)
                                  .Text(text =>
                                  {
                                      text.Span("10. Environmental Status: ")
                                          .FontSize(fontSize)
                                          .Bold();
                                  });
                            column.Item()
                                  .PaddingTop(10)
                                  .Row(row =>
                                  {
                                      row.RelativeItem().BorderBottom(0.25f).Text("");
                                  });
                            column.Item()
                                  .PaddingTop(8)
                                  .Text(text =>
                                  {
                                      text.Span("11. Right of Way Status: ")
                                          .FontSize(fontSize)
                                          .Bold();
                                  });
                            column.Item()
                                  .PaddingTop(10)
                                  .Row(row =>
                                  {
                                      row.RelativeItem().BorderBottom(0.25f).Text("");
                                  });
                            column.Item()
                                  .PaddingTop(8)
                                  .Text(text =>
                                  {
                                      text.Span("12. District discussion/comments on present and future impacts to the state system: ")
                                          .FontSize(fontSize)
                                          .Bold();
                                      text.Span("To be added by TxDOT personnel")
                                          .FontSize(fontSize)
                                          .Italic();
                                  });
                            column.Item()
                                  .PaddingTop(10)
                                  .Row(row =>
                                  {
                                      row.RelativeItem().BorderBottom(0.25f).Text("");
                                  });
                        });
                });
            });         
            document.GeneratePdf(fileName);
        }
    }
}
