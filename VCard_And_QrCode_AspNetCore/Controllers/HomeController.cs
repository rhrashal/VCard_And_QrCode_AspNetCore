using Microsoft.AspNetCore.Mvc;
using MixERP.Net.VCards;
using MixERP.Net.VCards.Models;
using MixERP.Net.VCards.Serializer;
using MixERP.Net.VCards.Types;
using System.Diagnostics;
using System.Drawing;
using VCard_And_QrCode_AspNetCore.Models;
using ZXing;
using ZXing.QrCode;

namespace VCard_And_QrCode_AspNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public HomeController(ILogger<HomeController> logger/*, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment*/)
        {
            _logger = logger;
            //_hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string QrCodeString)
        {

            var vcard = new VCard
            {
                Version = VCardVersion.V3,
                FirstName = "Robin",
                LastName = "Hood",
                Organization = "Sherwood Inc.",
                Addresses = new List<Address>
                {
                    new Address {
                        Type = AddressType.Work,
                        Street = "The Major Oak",
                        Locality = "Sherwood Forest",
                        PostalCode = "NG21 9RN",
                        Country = "United Kingdom",
                    }
                },
                Telephones = new List<Telephone>
                {
                    new Telephone {
                        Type = TelephoneType.Work,
                        Number = "+441623677321"
                    },
                    new Telephone
                    {
                        Type = TelephoneType.Home,
                        Number = "+441623677322"
                    }
                },
                Emails = new List<Email>
                {
                    new Email
                    {
                        Type = EmailType.Smtp,
                        EmailAddress = QrCodeString//"robin.hood@sherwoodinc.co.uk"
                    }
                }
            };


            var writer = new QRCodeWriter();
            //generate QR Code
            var resultBit = writer.encode(vcard.Serialize(), BarcodeFormat.QR_CODE, 200, 200);
            //get Bitmatrix result
            var matrix = resultBit;

            //convert bitmatrix into image 
            int scale = 2;

            Bitmap result = new Bitmap(matrix.Width * scale, matrix.Height * scale);
            for (int x = 0; x < matrix.Height; x++)
            {
                for (int y = 0; y < matrix.Width; y++)
                {
                    Color pixel = matrix[x, y] ? Color.Black : Color.White;
                    for (int i = 0; i < scale; i++)
                        for (int j = 0; j < scale; j++)
                            result.SetPixel(x * scale + i, y * scale + j, pixel);
                }
            }                
            ViewBag.Result = BitmapToBytesCode(result);


            //var qrCode = QrCode.EncodeText(vcard.Serialize(), QrCode.Ecc.Medium);
            //File.WriteAllText("vcard-qrcode.svg", qrCode.ToSvgString(3));



            return View();
        }

        private static Byte[] BitmapToBytesCode(Bitmap image)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


/// https://www.c-sharpcorner.com/article/generate-qr-code-in-net-core-using-bitmap/
/// 

//## install package : MixERP.Net.VCards

//## install package : System.Drawing.Common

//## install package : ZXing.Net.Bindings.Windows.Compatibility