using IntegratedImplementation.DTOS.DWM;
using IntegratedImplementation.Interfaces.DWM;
using iTextSharp.text;
using iTextSharp.text.pdf;
using QRCoder;
using SixLabors.ImageSharp;
using System.Drawing;
using System.IO;


namespace IntegratedImplementation.Services.DWM
{
    public class QRCodeGenerateService : IQRCodeGenerateService
    {

        public async Task<qrCodeResponse> GeneratePdf(QRCodeDto[] qRCodes)
        {
            try
            {
                Document document = new Document();
                MemoryStream memoryStream = new MemoryStream();
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                int numberOfQRCodes = qRCodes.Length;

                // Define the number of columns and rows in the grid
                int columns = 6; // Adjust as needed
                int rows = (int)Math.Ceiling((double)numberOfQRCodes / columns);

                // Calculate the size of each QR code
                float qrCodeSize = 100f; // Adjust as needed

                // Create the table to hold the QR codes
                PdfPTable table = new PdfPTable(columns);
                table.WidthPercentage = 100;
                //table.SetTotalWidth(new float[] { qrCodeSize, qrCodeSize, qrCodeSize });
                //table.LockedWidth = true;

                for (int i = 0; i < numberOfQRCodes; i++)
                {
                    var result = await GenerateQRCode(qRCodes[i].CustomerName, qRCodes[i].CustId);
                    var qrCodeImage = result;

                    using (MemoryStream bitmapStream = new MemoryStream(qrCodeImage))
                    {
                        Bitmap bitmap = new Bitmap(bitmapStream);
                        // Resize the QR code image
                        bitmap = new Bitmap(bitmap, (int)qrCodeSize, (int)qrCodeSize);

                        // Convert the resized image to iTextSharp Image
                        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(bitmap, System.Drawing.Imaging.ImageFormat.Png);

                        // Create a PdfPCell to hold the image and customer name
                        PdfPCell cell = new PdfPCell();
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        //cell.FixedHeight = qrCodeSize;

                        // Create a PdfPTable to hold the image and customer name
                        PdfPTable nestedTable = new PdfPTable(1);
                        nestedTable.WidthPercentage = 100;

                        // Create a PdfPCell for the image
                        PdfPCell imageCell = new PdfPCell(image);
                        imageCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        imageCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        imageCell.Border = PdfPCell.NO_BORDER;

                        // Add the imageCell to the nestedTable
                        nestedTable.AddCell(imageCell);

                        PdfPCell nameCell = new PdfPCell();
                        nameCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        nameCell.VerticalAlignment = Element.ALIGN_TOP; // Change to ALIGN_BOTTOM if you want the name at the bottom
                        nameCell.Border = PdfPCell.NO_BORDER;

                        // Create a Paragraph for the customer name
                        Paragraph nameParagraph = new Paragraph(qRCodes[i].CustId);
                        nameParagraph.Alignment = Element.ALIGN_CENTER;

                        // Set the top padding of the nameCell to push the content to the top
                        nameCell.PaddingTop = 5f; // Adjust the padding as needed

                        // Add the Paragraph to the nameCell
                        nameCell.AddElement(nameParagraph);

                        // Add the nameCell to the nestedTable
                        nestedTable.AddCell(nameCell);

                        // Add the nestedTable to the cell
                        cell.AddElement(nestedTable);

                        // Add the cell to the table
                        table.AddCell(cell);
                    }
                }
                int remainingCells = (rows * columns) - numberOfQRCodes;
                if (remainingCells > 0)
                {
                    PdfPCell emptyCell = new PdfPCell();
                    emptyCell.Colspan = remainingCells;
                    emptyCell.Border = PdfPCell.NO_BORDER;
                    table.AddCell(emptyCell);
                }

                // Add the table to the document
                document.Add(table);



                document.Close();





                return new qrCodeResponse
                {
                    data = memoryStream.ToArray(),
                    //length= memoryStream.Length.ToString(),
                };
            }catch (Exception ex)
            {
                return new qrCodeResponse
                {
                    //data = memoryStream.ToArray(),
                    //length= memoryStream.Length.ToString(),
                };
            }



                


        }

        public async Task<byte[]> GenerateQRCode(string customerName, string custId)
        {
            string text = custId + "," + customerName;

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);

            using (MemoryStream stream = new MemoryStream())
            {
                qrCode.GetGraphic(20).SaveAsPng(stream);
                byte[] qrCodeBytes = stream.ToArray();


                return qrCodeBytes;
            }

        }

        public class qrCodeResponse {

           public byte[] data { get; set; }
           public string length { get; set; }
            }
    }
    
}


