using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//iTextSharp
namespace ExportPDF.Mvc.Controllers
{
    public class PdfController : Controller
    {
        // GET: Pdf    
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
          
        [HttpGet]
        public ActionResult GerarPDF()
        {
            return Pdf();
        }

        #region GerarPDF
        private FileStreamResult Pdf()
        {
            MemoryStream workStream = new MemoryStream();
            Document document = new Document();
            PdfWriter.GetInstance(document, workStream).CloseStream = false;
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            Font times = new Font(bfTimes, 12, Font.NORMAL, BaseColor.RED);
            Chunk titulo = new Chunk("TITULO DO RELATORIO");

            //string imagepath = Server.MapPath("~/Imagens");
            //Image gif = Image.GetInstance(imagepath + "/logo.gif");

            document.Open();
            //document.Add(gif);

            document.Add(new Paragraph("______________________________________________________________________________"));
            document.Add(new Paragraph(titulo));
            document.Add(new Paragraph("______________________________________________________________________________"));

            PdfPTable table = new PdfPTable(4);
            table.TotalWidth = 540f;
            table.LockedWidth = true;

            //exemplo para usar tabela
            float[] widths = new float[] { 20f, 60f, 15f, 10f };
            table.SetWidths(widths);
            table.HorizontalAlignment = 0;
            table.SpacingBefore = 20f;
            table.SpacingAfter = 30f;

            table.AddCell("Código");
            table.AddCell("Descrição");
            table.AddCell("Coluna a");
            table.AddCell("Coluna b");

            table.AddCell("1");
            table.AddCell("teste");
            table.AddCell("2");
            table.AddCell("4");

            document.Add(table);
            document.Close();

            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;

            //return new FileContentResult(workStream, "application/pdf");
            return File(workStream, "application/pdf", "Teste.pdf");

        }
        #endregion
    }
}