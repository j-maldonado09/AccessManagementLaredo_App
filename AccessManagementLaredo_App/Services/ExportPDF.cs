using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using AccessManagementLaredo.HelperModels;
using Telerik.SvgIcons;

namespace AccessManagementLaredo_App.Services
{
    public class ExportPDF
    {
        public void CreatePdf(string physicalPath)
        {
            string fileName = Path.Combine(physicalPath, "PermitRequestPDFs", "PermitRequest.pdf");
            float narrowBorder = 0.50f;
            float thickBorder = 1f;
            float fontSize = 8.5f;

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
                    page.Content()                        
                        .PaddingTop(12)
                        .PaddingLeft(20)
                        .PaddingRight(20)
                        .Column(column =>
                        {
                            column.Item()
                                  .Table(table =>
                                  {
                                      table.ColumnsDefinition(columns =>
                                      {
                                          columns.RelativeColumn();
                                          columns.RelativeColumn();
                                          columns.RelativeColumn();
                                          columns.RelativeColumn();
                                          columns.RelativeColumn();
                                      });

                                      table.Cell().Row(1).ColumnSpan(5).Border(thickBorder).AlignCenter().Text("PERMIT NUMBER:").Bold().FontSize(fontSize);
                                      table.Cell().Row(2).Column(1).ColumnSpan(2).BorderVertical(thickBorder).BorderBottom(narrowBorder).AlignCenter().Text("").FontSize(fontSize);
                                      table.Cell().Row(2).Column(3).BorderVertical(thickBorder).BorderBottom(narrowBorder).AlignCenter().Text("*Attach kmz or kml file, OR").Bold().FontSize(fontSize);
                                      table.Cell().Row(2).Column(4).ColumnSpan(2).BorderVertical(thickBorder).BorderBottom(narrowBorder).AlignCenter().Text("ROADWAY").Bold().FontSize(fontSize);
                                      table.Cell().Row(3).Column(1).ColumnSpan(2).BorderVertical(thickBorder).BorderBottom(narrowBorder).AlignCenter().AlignMiddle().Text("REQUESTOR").FontSize(fontSize);
                                      table.Cell().Row(3).Column(3).BorderVertical(thickBorder).BorderBottom(narrowBorder).AlignCenter().AlignMiddle().Text("provide GPS Lat./Long.").Bold().FontSize(fontSize);
                                      table.Cell().Row(3).Column(4).BorderBottom(narrowBorder).BorderRight(narrowBorder).AlignCenter().AlignMiddle().Text("HWY NAME").Bold().FontSize(fontSize);
                                      table.Cell().Row(3).Column(5).BorderBottom(narrowBorder).BorderRight(thickBorder).AlignCenter().AlignMiddle().Text("");
                                      table.Cell().Row(4).Column(1).BorderLeft(thickBorder).BorderBottom(narrowBorder).BorderRight(narrowBorder).Text("");
                                      table.Cell().Row(4).Column(2).BorderBottom(narrowBorder).Text("");
                                      table.Cell().Row(4).Column(3).BorderVertical(thickBorder).BorderBottom(thickBorder).Text("").FontSize(fontSize);
                                      table.Cell().Row(4).Column(4).ColumnSpan(2).BorderBottom(narrowBorder).BorderRight(thickBorder).AlignCenter().AlignMiddle().Text("FOR TxDOT'S USE").Bold().FontSize(fontSize);
                                      table.Cell().Row(5).RowSpan(2).Column(1).BorderBottom(narrowBorder).BorderRight(narrowBorder).BorderLeft(thickBorder).AlignRight().AlignBottom().PaddingRight(4).Text("NAME").FontSize(fontSize);
                                      table.Cell().Row(5).RowSpan(2).Column(2).ColumnSpan(2).BorderBottom(narrowBorder).AlignLeft().AlignMiddle().PaddingLeft(4).Text("").FontSize(fontSize);
                                      table.Cell().Row(5).Column(4).BorderLeft(thickBorder).BorderBottom(narrowBorder).BorderRight(narrowBorder).AlignCenter().Text("CONTROL").Bold().FontSize(fontSize);
                                      table.Cell().Row(5).Column(5).BorderBottom(narrowBorder).BorderRight(thickBorder).AlignCenter().Text("").FontSize(fontSize);
                                      table.Cell().Row(6).Column(4).BorderLeft(thickBorder).BorderBottom(thickBorder).BorderRight(narrowBorder).AlignCenter().Text("SECTION").Bold().FontSize(fontSize);
                                      table.Cell().Row(6).Column(5).BorderBottom(thickBorder).BorderRight(thickBorder).AlignCenter().Text("").FontSize(fontSize);
                                      table.Cell().Row(7).Column(1).BorderLeft(thickBorder).BorderBottom(narrowBorder).BorderRight(narrowBorder).AlignRight().PaddingRight(4).Text("MAILING ADDRESS").FontSize(fontSize);
                                      table.Cell().Row(7).Column(2).ColumnSpan(4).BorderBottom(narrowBorder).BorderRight(thickBorder).AlignLeft().PaddingLeft(4).Text("").FontSize(fontSize);
                                      table.Cell().Row(8).Column(1).BorderLeft(thickBorder).BorderBottom(narrowBorder).BorderRight(narrowBorder).AlignRight().PaddingRight(4).Text("CITY, STATE, ZIP").FontSize(fontSize);
                                      table.Cell().Row(8).Column(2).ColumnSpan(4).BorderBottom(narrowBorder).BorderRight(thickBorder).AlignLeft().PaddingLeft(4).Text("").FontSize(fontSize);
                                      table.Cell().Row(9).Column(1).BorderLeft(thickBorder).BorderBottom(thickBorder).BorderRight(narrowBorder).AlignRight().PaddingRight(4).Text("PHONE NUMBER").FontSize(fontSize);
                                      table.Cell().Row(9).Column(2).ColumnSpan(4).BorderRight(thickBorder).BorderBottom(thickBorder).AlignLeft().PaddingLeft(4).Text("").FontSize(fontSize);
                                      table.Cell().Row(10).Column(1).BorderLeft(thickBorder).BorderBottom(thickBorder).BorderRight(narrowBorder).AlignRight().PaddingRight(4).Text("EMAIL ADDRESS").FontSize(fontSize);
                                      table.Cell().Row(10).Column(2).ColumnSpan(4).BorderRight(thickBorder).BorderBottom(thickBorder).AlignLeft().PaddingLeft(4).Text("").FontSize(fontSize);
                                      table.Cell().Row(11).Column(1).ColumnSpan(5).BorderVertical(thickBorder).BorderBottom(thickBorder).Padding(5).Text("* LOCATION OR COORDINATES AT INTERSECTION OF DRIVEWAY CENTERLINE WITH ABUTTING ROADWAY").Bold().FontSize(fontSize-1);
                                  });
                            column.Item()
                                  .PaddingTop(4)
                                  .Row(row =>
                                  {
                                      row.AutoItem().Text("The Texas Department of Transportation, hereinafter called the State, hereby authorizes").FontSize(fontSize);
                                      row.Spacing(2);
                                      row.RelativeItem().BorderBottom(0.25f).Text("").FontSize(fontSize);
                                      row.Spacing(2);
                                      row.AutoItem().Text(",").FontSize(fontSize);
                                  });
                            column.Item()
                                  .Row(row =>
                                  {
                                      row.AutoItem().Text("hereinafter called the Permittee (i.e., property owner)").FontSize(fontSize);
                                      row.Spacing(2);
                                      row.AutoItem().Container().AlignMiddle().Width(8).Height(8).Border(0.25f);
                                      row.Spacing(2);
                                      row.AutoItem().Text("construct /").FontSize(fontSize);
                                      row.Spacing(2);
                                      row.AutoItem().Container().AlignMiddle().Width(8).Height(8).Border(0.25f);
                                      row.Spacing(2);
                                      row.AutoItem().Text("reconstruct a").FontSize(fontSize);
                                      row.Spacing(2);
                                      row.RelativeItem().BorderBottom(0.25f).Text("").FontSize(fontSize);
                                      row.Spacing(2);
                                      row.AutoItem().Text("(residential, convenience ").FontSize(fontSize);
                                  });
                            column.Item()
                                  .Row(row =>
                                  {
                                      row.AutoItem().Text("store, retail mall, farm, etc.) access driveway on the highway right of way abutting highway number").FontSize(fontSize);
                                      row.Spacing(2);
                                      row.RelativeItem().BorderBottom(0.25f).Text("").FontSize(fontSize);
                                      row.Spacing(2);
                                      row.AutoItem().Text("in").FontSize(fontSize);
                                      row.Spacing(2);
                                      row.RelativeItem().BorderBottom(0.25f).Text("").FontSize(fontSize);
                                  });
                            column.Item().Column(col =>
                            {
                                col.Item().Row(row =>
                                {
                                    row.AutoItem().Text("County, located").FontSize(fontSize);
                                    row.Spacing(2);
                                    row.RelativeItem().BorderBottom(0.25f).Text("").FontSize(fontSize);
                                    row.Spacing(2);
                                    row.AutoItem().Text(".").FontSize(fontSize);
                                });

                                col.Item().PaddingTop(2).AlignCenter().Text("USE ADDITIONAL SHEETS AS NEEDED").FontSize(fontSize - 2);
                            });
                            column.Item()
                                  .PaddingTop(18)
                                  .Row(row =>
                                  {
                                      row.AutoItem().Text("Is this parcel in current litigation with the State of Texas?").FontSize(fontSize);
                                      row.ConstantItem(5);
                                      row.ConstantItem(10).Container().AlignMiddle().Width(8).Height(8).Border(0.25f);
                                      row.AutoItem().Text("YES").FontSize(fontSize);
                                      row.ConstantItem(10);
                                      row.ConstantItem(10).Container().AlignMiddle().Width(8).Height(8).Border(0.25f);
                                      row.AutoItem().Text("NO").FontSize(fontSize);
                                      row.ConstantItem(10);
                                      row.AutoItem().Text(" (If Yes, TxDOT will coordinate with District ROW Office.)").FontSize(fontSize);
                                  });
                            column.Item()
                                  .PaddingTop(18)
                                  .Row(row =>
                                  {
                                      row.AutoItem().Text("Is the Permittee or a family member of Permittee an employee or official of the Texas Department of Transportation?").FontSize(fontSize);
                                      row.ConstantItem(5);
                                      row.ConstantItem(10).Container().AlignMiddle().Width(8).Height(8).Border(0.25f);
                                      row.AutoItem().Text("YES").FontSize(fontSize);
                                      row.ConstantItem(10);
                                      row.ConstantItem(10).Container().AlignMiddle().Width(8).Height(8).Border(0.25f);
                                      row.ConstantItem(10);
                                      row.AutoItem().Text("NO").FontSize(fontSize);
                                  });
                            column.Item()
                                  .Row(row =>
                                  {
                                      row.AutoItem().Text("(If Yes, name of employee or official").FontSize(fontSize);
                                      row.Spacing(2);
                                      row.RelativeItem().BorderBottom(0.25f).Text("").FontSize(fontSize);
                                  });
                            column.Item()
                                  .PaddingTop(18)
                                  .Row(row =>
                                  {
                                      row.AutoItem().Text("Does an employee or official of the Texas Department of Transportation serve as an employee or officer of Permittee or own a controlling").FontSize(fontSize);                                      
                                  });
                            column.Item()
                                  .Row(row =>
                                  {
                                      row.AutoItem().Text("interest in Permittee?").FontSize(fontSize);
                                      row.ConstantItem(5);
                                      row.ConstantItem(10).Container().AlignMiddle().Width(8).Height(8).Border(0.25f);
                                      row.AutoItem().Text("YES").FontSize(fontSize);
                                      row.ConstantItem(10);
                                      row.ConstantItem(10).Container().AlignMiddle().Width(8).Height(8).Border(0.25f);
                                      row.AutoItem().Text("NO").FontSize(fontSize);
                                      row.ConstantItem(10);
                                      row.AutoItem().Text("(If Yes, name of employee or official").FontSize(fontSize);
                                      row.Spacing(2);
                                      row.RelativeItem().BorderBottom(0.25f).Text("").FontSize(fontSize);
                                  });
                            column.Item()
                                  .PaddingTop(18)
                                  .Row(row =>
                                  {
                                      row.AutoItem().Text("This permit is subject to the Access Driveway Policy described on page 2 and the following:").FontSize(fontSize);                                      
                                  });
                            column.Item()
                                  .PaddingTop(8)
                                  .Row(row =>
                                  {
                                      row.ConstantItem(15).Text("1.").FontSize(fontSize);
                                      row.RelativeItem().Text("The undersigned hereby agrees to comply with the terms and conditions set forth in this permit for construction and maintenance of an access driveway on the state highway right of way.").FontSize(fontSize);
                                  });
                            column.Item()
                                  .PaddingTop(8)
                                  .Row(row =>
                                  {
                                      row.ConstantItem(15).Text("2.").FontSize(fontSize);
                                      row.RelativeItem().Text("The Permittee represents that the design of the facilities, as shown in the attached design sketch, is in accordance with the Roadway Design Manual, Hydraulic Design Manual and the access management standards set forth in the Access Management Manual (except as otherwise permitted by an approved variance).").FontSize(fontSize);
                                  });
                            column.Item()
                                  .PaddingTop(8)
                                  .Row(row =>
                                  {
                                      row.ConstantItem(15).Text("3.").FontSize(fontSize);
                                      row.RelativeItem().Text("Construction of the driveway shall be in accordance with the attached design sketch, and is subject to inspection and approval by the State.").FontSize(fontSize);
                                  });
                            column.Item()
                                  .PaddingTop(8)
                                  .Row(row =>
                                  {
                                      row.ConstantItem(15).Text("4.").FontSize(fontSize);
                                      row.RelativeItem().Text("Maintenance of facilities constructed hereunder shall be the responsibility of the Permittee, and the State reserves the right to require any " +
                                          "changes, maintenance or repairs as may be necessary to provide protection of life or property on or adjacent to the highway. Changes in " +
                                          "design will be made only with prior written approval of the State. The department shall maintain all portions of public driveways that lie " +
                                          "within the state highway right of way and that connect to highways that are the maintenance responsibility of the department.").FontSize(fontSize);
                                  });
                            column.Item()
                                  .PaddingTop(8)
                                  .Row(row =>
                                  {
                                      row.ConstantItem(15).Text("5.").FontSize(fontSize);
                                      row.RelativeItem().Text("The Permittee shall hold harmless the State and its duly appointed agents and employees against any action for personal injury or property " +
                                          "damage related to the driveway permitted hereunder.").FontSize(fontSize);
                                  });
                            column.Item()
                                  .PaddingTop(8)
                                  .Row(row =>
                                  {
                                      row.ConstantItem(15).Text("6.").FontSize(fontSize);
                                      row.RelativeItem().Text("Except for regulatory and guide signs at county roads and city streets, the Permittee shall not erect any sign on or extending over any " +
                                          "portion of the highway right of way. The Permittee shall ensure that any vehicle service fixtures such as fuel pumps, vendor stands, or tanks " +
                                          "shall be located at least 12 feet from the right of way line to ensure that any vehicle services from these fixtures will be off the highway right " +
                                          "of way.").FontSize(fontSize);
                                  });
                            column.Item()
                                  .PaddingTop(8)
                                  .Row(row =>
                                  {
                                      row.ConstantItem(15).Text("7.").FontSize(fontSize);
                                      row.RelativeItem().Text("The State reserves the right to require a new access driveway permit in the event of: (i) a material change in land use, driveway traffic volume " +
                                          "or vehicle types using the driveway, or (ii) reconstruction or other modification of the highway facility by the State.").FontSize(fontSize);
                                  });
                            column.Item()
                                  .PaddingTop(8)
                                  .Row(row =>
                                  {
                                      row.ConstantItem(15).Text("8.").FontSize(fontSize);
                                      row.RelativeItem().Text("The State may revoke this permit upon violation of any provision of this permit by the Permittee.").FontSize(fontSize);
                                  });
                            column.Item()
                                  .PaddingTop(8)
                                  .Row(row =>
                                  {
                                      row.ConstantItem(15).Text("9.").FontSize(fontSize);
                                      row.RelativeItem().Text("This permit will become null and void if the above-referenced driveway facilities are not constructed within one year from the issuance date " +
                                          "of this permit.").FontSize(fontSize);
                                  });
                            column.Item()
                                  .PaddingTop(8)
                                  .Row(row =>
                                  {
                                      row.ConstantItem(15).Text("10.").FontSize(fontSize);
                                      row.AutoItem().Text("The Permittee will contact the State’s representative").FontSize(fontSize);
                                      row.Spacing(2);
                                      row.RelativeItem().BorderBottom(0.25f).Text("").FontSize(fontSize);
                                  });
                            column.Item()
                                  .Row(row =>
                                  {
                                      row.ConstantItem(15).Text(" ").FontSize(fontSize);
                                      row.AutoItem().Text("telephone, (").FontSize(fontSize);
                                      row.RelativeItem(0.25f).BorderBottom(0.25f).Text("").FontSize(fontSize);
                                      row.AutoItem().Text(")").FontSize(fontSize);
                                      row.Spacing(2);
                                      row.RelativeItem().BorderBottom(0.25f).Text("").FontSize(fontSize);
                                      row.AutoItem().Text(", at least twenty-four (24) hours prior to beginning the work authorized by this permit.").FontSize(fontSize);
                                  });
                            column.Item()
                                  .PaddingTop(8)
                                  .Row(row =>
                                  {
                                      row.ConstantItem(15).Text("11.").FontSize(fontSize);
                                      row.RelativeItem().Text(" The requesting Permittee will be provided instructions on the appeal process if this permit request is denied by the State. Note, a driveway " +
                                          "involving an Access Denial Line (ADL) does not have a right to appeal.").FontSize(fontSize);
                                  });
                        });
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
                    page.Content()
                        .PaddingTop(12)
                        .PaddingLeft(20)
                        .PaddingRight(20)
                        .Column(column =>
                        {                        
                            column.Item().Text("The undersigned hereby agrees to comply with the terms and conditions set forth in this permit for construction and maintenance of an access driveway on the highway right of way.").FontSize(fontSize);
                            column.Item().Column(col =>
                            {
                                col.Item()
                                   .PaddingTop(18)
                                   .Row(row =>
                                    {
                                        row.AutoItem().Text("Date:").FontSize(fontSize);
                                        row.Spacing(2);
                                        row.ConstantItem(150).BorderBottom(0.25f).Text("").FontSize(fontSize);
                                        row.ConstantItem(30);
                                        row.AutoItem().Text("Signed:").FontSize(fontSize);
                                        row.Spacing(2);
                                        row.RelativeItem().BorderBottom(0.25f).Text("").FontSize(fontSize);
                                    });
                                col.Item()
                                   .PaddingTop(2)
                                   .Row(row =>
                                   {
                                       row.ConstantItem(300);
                                       row.AutoItem().AlignRight().Text("(Property owner or owner's representative)").FontSize(fontSize-2);
                                   });
                            });
                            column.Item()
                                  .PaddingTop(40)
                                  .AlignCenter()
                                  .Text("Access Driveway Policy")
                                  .ExtraBold()
                                  .FontSize(fontSize);
                            column.Item()
                                  .PaddingTop(8)
                                  .Text("Title 43 Texas Administrative Code (Transportation), Part 1 (Texas Department of Transportation) Chapter 11 (Design), Subchapter C (Access " +
                                  "Connections To State Highways) and the \"Access Management Manual\" establish policy for the granting of access and the design, materials and " +
                                  "construction of driveways connecting to state highways. All driveway facilities must follow this policy. To the extent there is any conflict " +
                                  "between this permit and the policy, the policy shall control. If a proposed driveway does not comply with the access management standards, " +
                                  "the owner may seek a variance to a requirement contained in the access management standards by contacting the local TxDOT office.")
                                  .FontSize(fontSize);
                            column.Item()
                                  .PaddingTop(12)
                                  .Text("As to driveway permits that are issued under §11.59 of Subchapter C (Access Connections To State Highways), no rights of access are conveyed " +
                                  "by issuance of a driveway permit. Issuance of a driveway permit under this section does not convey any property right, including a right of " +
                                  "access to the highway facility. The department, in its sole discretion, may revoke a permit issued under this section on its determination that the " +
                                  "access location is needed for a highway purpose. Such a revocation may not be the basis for any claim of a constitutional taking of property for " +
                                  "the loss of access to the highway facility.")
                                  .FontSize(fontSize);
                            column.Item()
                                   .BorderBottom(0.25f).Text("").FontSize(fontSize);
                            column.Item()
                                 .PaddingTop(8)
                                 .AlignCenter()
                                 .Text("TxDOT Driveway Permit Request Contact")
                                 .ExtraBold()
                                 .FontSize(fontSize);                            
                            column.Item()
                                  .PaddingTop(8)
                                  .Text(text =>
                                  {
                                      text.Span("For a local contact for your TxDOT Driveway Permit Request or variance request, visit: ").FontSize(fontSize);
                                      text.Hyperlink("http://www.txdot.gov/inside-txdot/district.html", "http://www.txdot.gov/inside-txdot/district.html")
                                          .FontSize(fontSize)
                                          .FontColor(Colors.Blue.Darken4)
                                          .Underline();
                                      text.Span(". You can select the respective District, and then select the District Contacts which will include the applicable Area Engineers.").FontSize(fontSize);
                                  });


                            column.Item()
                                  .BorderBottom(0.25f).Text("").FontSize(fontSize);
                            column.Item()
                                 .PaddingTop(8)
                                 .AlignCenter()
                                 .Text("Other Conditions")
                                 .ExtraBold()
                                 .FontSize(fontSize);
                            column.Item()
                                  .PaddingTop(8)
                                  .Text("In addition to Items 1 thru 11 on page 1 of this permit, the driveway facility shall also be in accordance with the attached design sketch and " +
                                  "subject to the following additional conditions stated below:")
                                  .FontSize(fontSize);
                            column.Item()
                                  .PaddingTop(8)
                                  .Container()
                                  .Height(100)
                                  .Border(0.25f);
                            column.Item()
                                  .BorderBottom(0.25f).Text("").FontSize(fontSize);
                            column.Item()
                                 .PaddingTop(8)
                                 .AlignCenter()
                                 .Text("Variance Documentation Justification")
                                 .Bold()
                                 .FontSize(fontSize+1);
                            column.Item()
                                  .PaddingTop(18)
                                  .Text("(A variance to any requirement contained in the access management standards may be grnated if justified in" +
                                  "accordance with an item below and approved by the district engineer, or the district engineer's designee.)")
                                  .Bold()
                                  .FontSize(fontSize+1);
                            column.Item()
                                  .PaddingTop(18)
                                  .Text("For a Variance request, please indicate which of the below are applicable, as required by TAC §11.52(e):")
                                  .FontSize(fontSize);
                            column.Item()
                                  .PaddingTop(8)
                                  .Row(row =>
                                  {
                                      row.ConstantItem(15).Container().AlignMiddle().Width(8).Height(8).Border(0.25f);
                                      row.RelativeItem()
                                         .Text("a significant negative impact to the owner's real property or its use will likely result from the denial of its request for the variance, " +
                                          "including the loss of reasonable access to the property or undue hardship on a business located on the property.")
                                         .FontSize(fontSize);                                     
                                  });
                            column.Item()
                                  .Row(row =>
                                  {
                                      row.ConstantItem(15).Container().AlignMiddle().Width(8).Height(8).Border(0.25f);
                                      row.RelativeItem()
                                         .Text("an unusual condition affecting the property exists that was not caused by the property owner and justifies the request for the variance.")
                                         .FontSize(fontSize);
                                  });
                            column.Item()
                                  .PaddingTop(8)
                                  .Text("For the conditions selected above, provide written justification below. (Attach additional sheets, if needed)")
                                  .FontSize(fontSize);
                            column.Item()
                                  .PaddingTop(2)
                                  .Container()
                                  .Height(70)
                                  .Border(0.25f);
                        });
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
                    page.Content()
                        .Padding(20)
                        .Column(column =>
                        {
                            column.Item()
                                  .Text("For TXDOT use below:")
                                  .ExtraBold()
                                  .FontSize(fontSize);
                            column.Item()
                                  .Text("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -")
                                  .ExtraBold()
                                  .FontSize(fontSize);
                            column.Item()
                                  .Text("For Variance denials, please indicate which of the below conditions, as provided in TAC §11.52(e), were determined:")
                                  .FontSize(fontSize);
                            column.Item()
                                  .PaddingTop(8)
                                  .Row(row =>
                                  {
                                      row.ConstantItem(15).Container().AlignMiddle().Width(8).Height(8).Border(0.25f);
                                      row.RelativeItem()
                                         .Text("adversely affect the safety, design, construction, mobility, efficient operation, or maintenance of the highway; or")
                                         .FontSize(fontSize);
                                  });
                            column.Item()
                                  .PaddingTop(8)
                                  .Row(row =>
                                  {
                                      row.ConstantItem(15).Container().AlignMiddle().Width(8).Height(8).Border(0.25f);
                                      row.RelativeItem()
                                         .Text("likely impair the ability of the state or the department to receive funds for highway construction or maintenance from the federal government.")
                                         .FontSize(fontSize);
                                  });
                            column.Item()
                                  .PaddingTop(8)
                                  .Text("For driveway permits to be issued under TAC §11.59:")
                                  .FontSize(fontSize);
                            column.Item()
                                 .Row(row =>
                                 {
                                     row.AutoItem().Text("Is this driveway crossing an access denial line?").FontSize(fontSize);
                                     row.ConstantItem(5);
                                     row.ConstantItem(10).Container().AlignMiddle().Width(8).Height(8).Border(0.25f);                                     
                                     row.AutoItem().Text("YES").FontSize(fontSize);
                                     row.ConstantItem(10);
                                     row.ConstantItem(10).Container().AlignMiddle().Width(8).Height(8).Border(0.25f);
                                     row.AutoItem().Text("NO").FontSize(fontSize);
                                 });
                            column.Item()
                                  .PaddingTop(8)
                                  .Row(row =>
                                  {
                                     row.AutoItem().Text("(If Yes, is this a ").FontSize(fontSize);
                                     row.ConstantItem(5);
                                     row.ConstantItem(10).Container().AlignMiddle().Width(8).Height(8).Border(0.25f);
                                     row.AutoItem().Text("private driveway or a").FontSize(fontSize);
                                     row.ConstantItem(5);
                                     row.ConstantItem(10).Container().AlignMiddle().Width(8).Height(8).Border(0.25f);
                                     row.AutoItem().Text("commercial driveway?)").FontSize(fontSize);
                                  });
                            column.Item()
                                 .PaddingTop(8)
                                 .Row(row =>
                                 {
                                     row.AutoItem().Text("Private Driveway Fee:").FontSize(fontSize);
                                     row.ConstantItem(5);
                                     row.ConstantItem(10).Container().AlignMiddle().Width(8).Height(8).Border(0.25f);
                                     row.AutoItem().Text("$250").FontSize(fontSize);
                                 });
                            column.Item()
                                 .PaddingTop(8)
                                 .Row(row =>
                                 {
                                     row.AutoItem().Text("Commercial Driveway Fee:").FontSize(fontSize);
                                     row.ConstantItem(5);
                                     row.ConstantItem(10).Container().AlignMiddle().Width(8).Height(8).Border(0.25f);
                                     row.AutoItem().Text("$2,500").FontSize(fontSize);
                                     row.ConstantItem(10);
                                     row.ConstantItem(10).Container().AlignMiddle().Width(8).Height(8).Border(0.25f);
                                     row.AutoItem().Text("$10,000").FontSize(fontSize);
                                     row.ConstantItem(10);
                                     row.ConstantItem(10).Container().AlignMiddle().Width(8).Height(8).Border(0.25f);
                                     row.AutoItem().Text("$25,000").FontSize(fontSize);
                                 });

                        });
                });

            });

            document.GeneratePdf(fileName);
        }
    }
}
