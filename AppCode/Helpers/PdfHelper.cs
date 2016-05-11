using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.IO;
using System.Net;
using System.Web;

namespace OWDARO.Helpers
{
    public static class PdfHelper
    {
        public static void GetPDFFromHTML(string HTML, string path, string filename)
        {
            //var document = new Document();

            //PdfWriter.GetInstance(document, new FileStream(Path.Combine(path, filename), FileMode.Create));

            //document.Open();            

            //var hw = new HTMLWorker(document);
            //hw.Parse(new StringReader(HTML));

            //document.Close();

            using (Document document = new Document())
            {
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(Path.Combine(path, filename), FileMode.Create));

                document.Open();

                XMLWorkerHelper.GetInstance().ParseXHtml(
                  writer, document, new StringReader(HTML)
                );
            }

            var req = new WebClient();
            var response = HttpContext.Current.Response;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Buffer = true;
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename);
            var data = req.DownloadData(path + filename);
            response.BinaryWrite(data);
            response.End();
        }
    }
}