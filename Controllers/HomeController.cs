using IronBarCode;
using Microsoft.AspNetCore.Mvc;
using QRCode.Models;
using System.Diagnostics;

namespace QRCode.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _environment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _environment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(QRCodeModel gerarQRCode)
        {
            try
            {
                GeneratedBarcode barcode = QRCodeWriter.CreateQrCode(gerarQRCode.QRCodeTexto, 200);
                barcode.AddBarcodeValueTextBelowBarcode();
                // aplicar estilo ao QR code e texto
                barcode.SetMargins(10);
                barcode.ChangeBarCodeColor(Color.BlueViolet);
                string path = Path.Combine(_environment.WebRootPath, "qrcodes");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string filePath = Path.Combine(_environment.WebRootPath, "qrcodes/qrcode.png");

                barcode.SaveAsPng(filePath);

                string fileName = Path.GetFileName(filePath);
                string imageUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/qrcodes/" + fileName;
                ViewBag.QrCodeUri = imageUrl;
            }
            catch (Exception)
            {
                throw;
            }
            return View();
        }
    }
}
