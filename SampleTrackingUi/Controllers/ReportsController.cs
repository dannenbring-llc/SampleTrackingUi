using Microsoft.AspNetCore.Mvc;
using SampleTrackingUi.Services;
using SampleTrackingUi.ViewModels.Reports;
using System.Threading.Tasks;

namespace SampleTrackingUi.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IIGTSamplesApi _igtSamplesApi;

        public ReportsController(IIGTSamplesApi igtSamplesApi)
        {
            _igtSamplesApi = igtSamplesApi;
        }

        public IActionResult TrayMap()
        {
            return View();
        }

        [HttpGet("ClearMini")]
        public async Task<IActionResult> ClearMini(ClearMiniViewModel viewModel)
        {
            if (viewModel.LogNumber != null)
            {
                var sample = await _igtSamplesApi.GetSampleAsync(viewModel.LogNumber);
                if (sample == null)
                {
                    viewModel.ShowReportButton = false;
                    return View(viewModel);
                }
                viewModel.LogNumber = sample.KbNumber;
                viewModel.PatId = sample.PatientId;
                viewModel.PatientName = sample.PatientName;
                viewModel.ShowReportButton = true;
            }

            return View(viewModel);
        }
    }
}