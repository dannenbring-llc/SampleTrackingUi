using Microsoft.AspNetCore.Mvc;
using SampleTrackingUi.Services;
using SampleTrackingUi.ViewModels.Reports;
using System.Threading.Tasks;
using System.Linq;
using SampleTrackingUi.ApiModels.Reports;
using AutoMapper;
using SampleTrackingUi.Entities.Printers;
using System.Collections.Generic;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using QRCoder;

namespace SampleTrackingUi.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IIGTSamplesApi _igtSamplesApi;
        private readonly IReportService _reportService;
        private readonly ISampleTrackingApi _sampleTrackingApi;
        private readonly IMapper _mapper;

        public ReportsController(IIGTSamplesApi igtSamplesApi, IReportService reportService, ISampleTrackingApi sampleTrackingApi, IMapper mapper)
        {
            _igtSamplesApi = igtSamplesApi;
            _reportService = reportService;
            _sampleTrackingApi = sampleTrackingApi;
            _mapper = mapper;
        }

        public IActionResult TrayMap()
        {
            return View();
        }

        [HttpGet("ClearMini")]
        public async Task<IActionResult> ClearMini(ClearMiniViewModel viewModel)
        {
            if (viewModel.Labels != null)
            {

                for (int i = 0; i < viewModel.Labels.Count; i++)
                {
                    if (string.IsNullOrEmpty(viewModel.Labels[i].PatientName))
                    {
                        var sample = await _igtSamplesApi.GetSampleAsync(viewModel.Labels[i].LogNumber);
                        if (sample == null)
                        {
                            viewModel.ShowReportButton = false;
                            return View(viewModel);
                        }
                        viewModel.Labels[i].LogNumber = sample.KbNumber;
                        viewModel.Labels[i].PatId = sample.PatientId;
                        viewModel.Labels[i].PatientName = sample.PatientName;
                        viewModel.ShowReportButton = true;
                    }
                }
                if (viewModel.ShowReportButton)
                {
                    viewModel.Labels.Add(new Label());
                }

                try
                {
                    viewModel.Printers = await _sampleTrackingApi.GetPrinters();
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
                
            }

            return View(viewModel);
        }

        [HttpPost("ClearMini")]
        public async Task<IActionResult> ClearMiniPost(ClearMiniViewModel viewModel)
        {
            return RedirectToAction("ClearMini", "Reports");
        }

        // GET: Home
        [HttpGet(Name = "Barcode")]
        public ActionResult Barcode()
        {
            return View();
        }

        [HttpPost(Name = "Barcode")]
        public ActionResult Barcode(string barcode)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                //The Image is drawn based on length of Barcode text.
                using (Bitmap bitMap = new Bitmap(barcode.Length * 40, 80))
                {
                    //The Graphics library object is generated for the Image.
                    using (Graphics graphics = Graphics.FromImage(bitMap))
                    {
                        //The installed Barcode font.
                        Font oFont = new Font("IDAutomationHC39M Free Version", 16);
                        PointF point = new PointF(2f, 2f);

                        //White Brush is used to fill the Image with white color.
                        SolidBrush whiteBrush = new SolidBrush(Color.White);
                        graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);

                        //Black Brush is used to draw the Barcode over the Image.
                        SolidBrush blackBrush = new SolidBrush(Color.Black);
                        graphics.DrawString("*" + barcode + "*", oFont, blackBrush, point);
                    }

                    //The Bitmap is saved to Memory Stream.
                    bitMap.Save(ms, ImageFormat.Png);

                    //The Image is finally converted to Base64 string.
                    ViewBag.BarcodeImage = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                }
            }

            return View();
        }

        // GET: Home
        [HttpGet(Name = "QrCode")]
        public ActionResult QrCode()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost(Name = "QrCode")]
        public IActionResult QrCode(string txtQRCode)
        {
            var _qrCode = new QRCodeGenerator();
            var _qrCodeData = _qrCode.CreateQrCode(txtQRCode, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCode(_qrCodeData);
            var qrCodeImage = qrCode.GetGraphic(20);
            return View(BitmapToBytesCode(qrCodeImage));
        }

        [NonAction]
        private static Byte[] BitmapToBytesCode(Bitmap image)
        {
            using var stream = new MemoryStream();
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            return stream.ToArray();
        }
    }
}