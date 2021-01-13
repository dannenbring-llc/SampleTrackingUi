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

    }
}