using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleTrackingUi.ApiModels.Scans;
using SampleTrackingUi.Services;
using SampleTrackingUi.ViewModels.SearchViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using SampleTrackingUi.ApiModels.Samples;
using SampleTrackingUi.Models.Scans;

namespace SampleTrackingUi.Controllers
{
    [Route("Search")]
    public class SearchController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISampleTrackingApi _sampleTrackingApi;
        private readonly IIGTSamplesApi _igtSamplesApi;
        private readonly ILogger<SearchController> _logger;

        public SearchController(IMapper mapper, ISampleTrackingApi sampleTrackingApi, IIGTSamplesApi igtSamplesApi, ILogger<SearchController> logger)
        {
            _mapper = mapper;
            _sampleTrackingApi = sampleTrackingApi;
            _igtSamplesApi = igtSamplesApi;
            _logger = logger;
        }

        //[HttpGet]
        //[Route("FindByKb")]
        //public IActionResult FindByKb()
        //{
        //    var vm = new FindByKbViewModel();
        //    return View(vm);
        //}


        [HttpGet]
        [Route("FindByKb")]
        public async Task<IActionResult> FindByKb(string trayDescription, string kbNumber, string patientName, DateTime logDate)
        {
            var vm = new FindByKbViewModel();
            //var sample = await _sampleTrackingApi.GetSerumTrayMapBySampleAsync(kbNumber);
            if (trayDescription != null || kbNumber != null || patientName != null || logDate != DateTime.MinValue)
            {
                //var tabluar = await _sampleTrackingApi.GetTrayMapDataAsync(kbNumber, trayDescription, patientName);
                //var tabDistinct = tabluar.Select(t => new TrayTabular( t.TrayDescription, t.SampleId, t.AliquotId, t.PatientName, t.TrayLocation )).Distinct();
                if (kbNumber != null)
                {
                    kbNumber = kbNumber.Trim();
                }

                if (trayDescription != null)
                {
                    trayDescription = trayDescription.Trim();
                }

                if (patientName != null)
                {
                    patientName = patientName.Trim();
                }

                vm.SearchResults = _mapper.Map<List<SearchResultBySampleApi>>(await _sampleTrackingApi.GetSerumTrayMapBySampleAsync(kbNumber, trayDescription, patientName, logDate)).Distinct().ToList();
                vm.TrayMapData = _mapper.Map<List<TrayMapDataApi>>(await _sampleTrackingApi.GetTrayMapDataAsync(kbNumber, trayDescription, patientName, logDate)).OrderBy(t => t.TrayLocation).OrderBy(t => Convert.ToInt32(t.TrayCol)).OrderBy(t => t.TrayRow).ToList();
            }
            return View(vm);
        }

        [HttpPost]
        [Route("FindByKb")]
        public IActionResult FindByKbPost()
        {
            return View();
        }

        [HttpGet]
        [Route("FindTray")]
        public IActionResult FindTray()
        {
            return View();
        }

        [HttpGet]
        [Route("FreezerMap")]
        public IActionResult FreezerMap()
        {
            return View();
        }

        [HttpGet]
        [Route("StaleSamples")]
        public async Task<IActionResult> StaleSamples(DateTime logDate)
        {
            var vm = new FindByKbViewModel();

            if (logDate != DateTime.MinValue)
            {
                vm.SearchResults = _mapper.Map<List<SearchResultBySampleApi>>(await _sampleTrackingApi.GetSerumTrayMapStaleSamplesAsync(logDate)).Distinct().ToList();
                vm.TrayMapData = _mapper.Map<List<TrayMapDataApi>>(await _sampleTrackingApi.GetTrayMapStaleSamplesAsync(logDate)).OrderBy(t => t.TrayLocation).OrderBy(t => Convert.ToInt32(t.TrayCol)).OrderBy(t => t.TrayRow).ToList();
            }
            else
            {
                vm.LogDate = DateTime.Now.AddMonths(-6).ToShortDateString();
            }
            return View(vm);
        }

        [HttpGet]
        [Route("FindPatient/{kbNumber?}")]
        public async Task<IActionResult> PatientLookup(PatientLookupViewModel vm)
        {
            if (vm.Sample.KbNumber != null)
            {
                var kbNumber = vm.Sample.KbNumber;
                vm.Sample = null;
                vm.Sample = _mapper.Map<SampleApi>(await _igtSamplesApi.GetSampleAsync(kbNumber));
            }
            return View(vm);
        }

    }
}