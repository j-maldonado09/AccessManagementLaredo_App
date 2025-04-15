using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using AccessManagementLaredo.HelperModels;

namespace AccessManagementLaredo_App.Services
{
    public class ExportPDF
    {
        public void CreatePdf(string physicalPath)
        {
            string fileName = Path.Combine(physicalPath, "PermitRequestPDFs", "PermitRequest.pdf");
            //string fileName = "";

            var document = Document.Create(container =>
            {
                // Page 1 ***************************************************
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(20);

                    // Header for Page 1
                    page.Header().Element(header =>
                    {
                        header.Image(Path.Combine(physicalPath, "images", "Form 1058 Header Page 1.png"));
                    });

                    // Content for Page 1
                    page.Content().Text("Content for Page 1");
                });

                // Page 2 ***************************************************
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(25);

                    // Header for Page 2
                    page.Header().Element(header =>
                    {
                        header.Image(Path.Combine(physicalPath, "images", "Form 1058 Header Page 2.png"));
                    });

                    // Content for Page 2
                    page.Content().Text("Content for Page 2");
                });

                // Page 3 ***************************************************
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(25);

                    // Header for Page 3
                    page.Header().Element(header =>
                    {
                        header.Image(Path.Combine(physicalPath, "images", "Form 1058 Header Page 3.png"));
                    });

                    // Content for Page 3
                    page.Content().Text("Content for Page 2");
                });

            });

            document.GeneratePdf(fileName);
        }
    }
}
