using HealthcareApp.Models.ViewModels;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace HealthcareApp.Utils
{
    public class PdfGenerator
    {
        public byte[]  GetPdf(List<AdmissionMedicalReport> admissionReportList)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                PdfDocument pdfDoc = new PdfDocument(new PdfWriter(ms));

                PdfFont regularFont = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
                PdfFont titleFont = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);
                Document doc = new Document(pdfDoc);



                foreach (AdmissionMedicalReport item in admissionReportList)
                {
                    var admission = item.PatientAdmission;
                    var medicalReport = item.MedicalReport;
                    if (admission is not null)
                    {
                        Table table = new Table(UnitValue.CreatePercentArray(3)).UseAllAvailableWidth().SetVerticalAlignment(VerticalAlignment.MIDDLE);
                        table.SetMarginTop(10);
                        table.AddCell(new Cell(1, 3).SetFont(titleFont).SetTextAlignment(TextAlignment.CENTER)
                             .SetBackgroundColor(ColorConstants.LIGHT_GRAY).Add(new Paragraph("Admission Information")));
                        table.AddCell(new Cell(1, 1).SetFont(titleFont).Add(new Paragraph("Patient Name")));
                        table.AddCell(new Cell(1, 2).SetFont(regularFont).Add(new Paragraph(admission.Patient?.FullName ?? "N/A")));
                        table.AddCell(new Cell(1, 1).SetFont(titleFont).Add(new Paragraph("Doctor Name")));
                        table.AddCell(new Cell(1, 2).SetFont(regularFont).Add(new Paragraph(admission.Doctor?.FullName ?? "N/A")));
                        table.AddCell(new Cell(1, 1).SetFont(titleFont).Add(new Paragraph("Date/Time")));
                        table.AddCell(new Cell(1, 2).SetFont(regularFont).Add(new Paragraph(admission.AdmissionDateTime.ToString())));
                        table.AddCell(new Cell(1, 1).SetFont(titleFont).Add(new Paragraph("Urgent")));
                        table.AddCell(new Cell(1, 2).SetFont(regularFont).Add(new Paragraph(admission.IsUrgent ? "Yes" : "No")));
                        table.AddCell(new Cell(1, 1).SetFont(titleFont).Add(new Paragraph("Cancelled")));
                        table.AddCell(new Cell(1, 2).SetFont(regularFont).Add(new Paragraph(admission.IsCancelled ? "Yes" : "No")));

                        table.AddCell(new Cell(1, 3).SetFont(titleFont).SetTextAlignment(TextAlignment.CENTER)
                             .SetBackgroundColor(ColorConstants.LIGHT_GRAY).Add(new Paragraph("Medical Report Information")));
                        table.AddCell(new Cell(1, 1).SetFont(titleFont).Add(new Paragraph("Creation Date")));
                        table.AddCell(new Cell(1, 2).SetFont(regularFont).Add(new Paragraph(medicalReport?.DateCreated.ToString() ?? "N/A")));
                        table.AddCell(new Cell(1, 1).SetFont(titleFont).Add(new Paragraph("Description")));
                        table.AddCell(new Cell(1, 2).SetFont(regularFont).Add(new Paragraph(medicalReport?.Description ?? "N/A")));

                        doc.Add(table);
                        doc.Add(new AreaBreak());
                    }
                }
                
                // trim last page
                var numberOfPages = pdfDoc.GetNumberOfPages();
                if (numberOfPages > 1)
                {
                    pdfDoc.RemovePage(numberOfPages);
                }

                doc.Close();
                return ms.ToArray();
            }
        }
    }
}
