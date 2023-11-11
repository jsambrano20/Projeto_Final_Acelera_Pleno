using FrontMVC.Models.Pedido;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System.IO;

namespace FrontMVC.Controllers
{
    public class ComprovanteController : Controller
    {
        private readonly IRazorViewEngine _razorViewEngine;
        public ComprovanteController(IRazorViewEngine razorViewEngine)
        {
            _razorViewEngine = razorViewEngine;
        }
        public IActionResult GerarPdf(string model)
        {

            var modelData = JsonConvert.DeserializeObject<List<PedidosMesa>>(model);



            var documento = new Document();
            var stream = new MemoryStream();

            var escritorPdf = PdfWriter.GetInstance(documento, stream);

            documento.Open();

            var viewHtml = this.RenderViewToString("ComprovantePDF", modelData);
            //var htmlParser = new HTMLWorker(documento);

            //htmlParser.Parse(new StringReader(viewHtml));

            if (string.IsNullOrEmpty(viewHtml))
            {
                return NotFound();
            }

            var htmlParser = new HTMLWorker(documento);
            try
            {
                htmlParser.Parse(new StringReader(viewHtml));
            }
            catch (Exception ex)
            {
                // Logue a exceção para ajudar na depuração
                Console.WriteLine($"Erro ao fazer parse do HTML: {ex.Message}");
                return BadRequest();
            }

            escritorPdf.CloseStream = false;

            documento.Close();

            stream.Position = 0;

            return File(stream, "application/pdf", $"Comprovante_{DateTime.Now.ToString("yyyyMMddHHmmss")}.pdf");
        }
        public string RenderViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = _razorViewEngine.FindView(ControllerContext, viewName, false);
                var viewContext = new ViewContext(
                    ControllerContext,
                    viewResult.View,
                    ViewData,
                    TempData,
                    sw,
                    new HtmlHelperOptions()
                );
                viewResult.View.RenderAsync(viewContext);
                return sw.GetStringBuilder().ToString();
            }
        }

    }
}
