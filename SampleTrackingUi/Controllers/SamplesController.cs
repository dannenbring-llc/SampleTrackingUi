using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SampleTrackingUi.ApiModels.Sessions;
using SampleTrackingUi.Models.Samples;
using SampleTrackingUi.Models.Sessions;
using SampleTrackingUi.Services;
using SampleTrackingUi.ViewModels.Samples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SampleTrackingUi.Controllers
{
    [Route("Samples")]
    public class SamplesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISampleTrackingApi _sampleTrackingApi;
        private readonly IIGTSamplesApi _igtSamplesApi;

        public SamplesController(IMapper mapper, ISampleTrackingApi sampleTrackingApi, IIGTSamplesApi igtSamplesApi)
        {
            _mapper = mapper;
            _sampleTrackingApi = sampleTrackingApi;
            _igtSamplesApi = igtSamplesApi;
            //UserId = Convert.ToInt32(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        [HttpGet("")]
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("SampleAliquot")]
        public async Task<IActionResult> SampleAliquot(SampleAliquotViewModel viewModel, string checkTray, string createTray)
        {

            viewModel.TrayExists = true;
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var session = await _sampleTrackingApi.GetSessionAsync(userId);

            //if (session != null)
            //{
            //    viewModel.Session = _mapper.Map<Session>(session);
            //}

            //if (viewModel.Session.StatusId == 'S')
            //{
            //    //Redirect to Scan Screen
            //    RedirectToAction();
            //}

            if (session != null)
            {
                viewModel.Session = _mapper.Map<Session>(session);
                return RedirectToAction("SelectSample", "Samples", new { trayId = session.TrayId, trayName = session.TrayCode });
            }

            //if (checkTray != "CheckTray")
            //{
            //    return View(viewModel);
            //}

            if (viewModel.Tray.Description == null)
            {
                return View(viewModel);
            }
            else if (viewModel.Tray.Description.Trim() == null)
            {
                return View(viewModel);
            }

            var openSession = new OpenSessionApi();
            //Create new Session
            openSession.UserId = userId;
            openSession.TrayCode = viewModel.Tray.Description;
            openSession.CloseExistingSession = true;
            openSession.SessionTypeId = 1;
            var newSession = await _sampleTrackingApi.OpenSessionAsync(openSession);
            viewModel.Session.SessionId = newSession.SessionId;
            viewModel.Session.UserId = newSession.UserId;
            viewModel.Tray.Description = newSession.TrayCode;
            viewModel.Tray.Id = newSession.TrayId;

            return RedirectToAction("SelectSample", "Samples", new { trayId = newSession.TrayId, trayName = newSession.TrayCode, newSession.SessionId });

        }

        [HttpGet]
        [Route("SelectSample")]
        public async Task<IActionResult> SelectSample(SelectSampleViewModel viewModel, int trayId, string trayName, int sessionId)
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var session = await _sampleTrackingApi.GetSessionAsync(userId);

            if (session.SessionId != 0)
            {
                viewModel.Session = _mapper.Map<Session>(session);
                viewModel.SampleAliquots = _mapper.Map<List<ApiModels.Samples.SampleAliquotApi>, List<SampleAliquot>>(await _sampleTrackingApi.GetAliquotsAsync(viewModel.Session.SessionId));
            }

            if (viewModel.Sample.KbNumber != null)
            {
                var sample = await _igtSamplesApi.GetSampleAsync(viewModel.Sample.KbNumber);
                if (sample == null)
                {
                    return View(viewModel);
                }
                viewModel.Sample.KbNumber = sample.KbNumber;
                viewModel.Sample.LogDateTime = sample.LogDateTime;
                viewModel.Sample.PatientName = sample.PatientName;
                if (sample.KbNumber != null)
                {
                    return RedirectToAction("SelectSubType", "Samples", new { kbNumber = sample.KbNumber, LogDateTime = sample.LogDateTime, PatientName = sample.PatientName, trayName = trayName, sessionId });
                }
            }
            viewModel.PageTitle = "Select Sample";
            return View(viewModel);
        }

        [HttpPost("SelectSample")]
        public IActionResult SelectSamplePost(SelectSampleViewModel viewModel, string trayName)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine("Post");
            }
            viewModel.PageTitle = "Select Sample";
            return RedirectToAction("SelectSubType");
        }

        [HttpGet]
        [Route("SelectSubType")]
        public async Task<IActionResult> SelectSubType(SelectSampleViewModel viewModel, string kbNumber, DateTime logDateTime, string patientName, int subTypeId, string trayName, int sessionId)
        {
            var UserId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var session = await _sampleTrackingApi.GetSessionAsync(UserId);
            viewModel.Session = _mapper.Map<Session>(session);

            viewModel.Sample.KbNumber = kbNumber;
            viewModel.Sample.PatientName = patientName;
            viewModel.Sample.LogDateTime = logDateTime;
            viewModel.Tray.Description = trayName;
            viewModel.PageTitle = "Select Sub Type";
            viewModel.SubTypes = _mapper.Map<List<ApiModels.Samples.SubTypeApi>, List<Models.Samples.SubType>>(await _sampleTrackingApi.GetSubTypesAsync());
            viewModel.SampleAliquots = _mapper.Map<List<ApiModels.Samples.SampleAliquotApi>, List<SampleAliquot>>(await _sampleTrackingApi.GetAliquotsAsync(viewModel.Session.SessionId));

            subTypeId = 1;

            if (subTypeId != 0)
            {
                return RedirectToAction("AddSampleAliquots", "Samples", new { kbNumber = kbNumber, LogDateTime = logDateTime, PatientName = patientName, SubTypeId = subTypeId, TrayName = trayName });
            }

            return View(viewModel);
        }

        [HttpPost]
        [Route("SelectSubType")]
        public async Task<IActionResult> SelectSubTypePost(SelectSampleViewModel viewModel, string kbNumber, DateTime logDateTime, string patientName, int subTypeId, string trayName)
        {
            viewModel.Sample.KbNumber = kbNumber;
            viewModel.Sample.PatientName = patientName;
            viewModel.Sample.LogDateTime = logDateTime;
            viewModel.Tray.Description = trayName;
            viewModel.PageTitle = "Select Sub Type";
            viewModel.SubTypes = _mapper.Map<List<ApiModels.Samples.SubTypeApi>, List<Models.Samples.SubType>>(await _sampleTrackingApi.GetSubTypesAsync());

            if (subTypeId != 0)
            {
                return RedirectToAction("AddSampleAliquots", "Samples", new { viewModel, kbNumber = kbNumber, LogDateTime = logDateTime, PatientName = patientName, SubTypeId = subTypeId, trayName = trayName });
            }

            return View(viewModel);
        }

        [HttpGet]
        [Route("AddSampleAliquots")]
        public async Task<IActionResult> AddSampleAliquots(SelectSampleViewModel viewModel, string kbNumber, DateTime logDateTime, string patientName, int subTypeId, string trayName)
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var session = await _sampleTrackingApi.GetSessionAsync(userId);
            viewModel.Session = _mapper.Map<Session>(session);

            viewModel.Sample.KbNumber = kbNumber;
            viewModel.Sample.PatientName = patientName;
            viewModel.Sample.LogDateTime = logDateTime;
            viewModel.Tray.Description = trayName;
            viewModel.PageTitle = "Select Sub Type";
            viewModel.Session = _mapper.Map<Session>(session);
            viewModel.SampleAliquots = _mapper.Map<List<ApiModels.Samples.SampleAliquotApi>, List<SampleAliquot>>(await _sampleTrackingApi.GetAliquotsAsync(viewModel.Session.SessionId));
            viewModel.SubTypes = _mapper.Map<List<ApiModels.Samples.SubTypeApi>, List<Models.Samples.SubType>>(await _sampleTrackingApi.GetSubTypesAsync());

            for (int i = 0; i < 20; i++)
            {
                viewModel.Scans.Add(null);
            }

            if (subTypeId != 0)
            {

            }

            return View(viewModel);
        }

        [HttpPost]
        [Route("AddSampleAliquots")]
        public IActionResult AddSampleAliquotsPost(SelectSampleViewModel viewModel, string kbNumber, DateTime logDateTime, string patientName, int subTypeId, List<string> scans, string trayName)
        {
            viewModel.Sample.KbNumber = kbNumber;
            viewModel.Sample.PatientName = patientName;
            viewModel.Sample.LogDateTime = logDateTime;
            viewModel.Tray.Description = trayName;
            viewModel.PageTitle = "Select Sub Type";

            return RedirectToAction("ValidateSampleAliqouts", "Samples", new { viewModel, kbNumber, LogDateTime = logDateTime, PatientName = patientName, SubTypeId = subTypeId, scans, TrayName = trayName });
        }

        [HttpGet]
        [Route("ValidateSampleAliquots")]
        public async Task<IActionResult> ValidateSampleAliqouts(SelectSampleViewModel viewModel, string kbNumber, DateTime logDateTime, string patientName, int subTypeId, List<string> scans, List<Scan> scanList, string trayName)
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var session = await _sampleTrackingApi.GetSessionAsync(userId);
            viewModel.Session = _mapper.Map<Session>(session);

            var reScansList = new List<Scan>();
            viewModel.Sample.KbNumber = kbNumber;
            viewModel.Sample.PatientName = patientName;
            viewModel.Sample.LogDateTime = logDateTime;
            viewModel.Tray.Description = trayName;
            viewModel.PageTitle = "Select Sub Type";
            viewModel.Session = _mapper.Map<Session>(session);
            viewModel.SubTypes = _mapper.Map<List<ApiModels.Samples.SubTypeApi>, List<Models.Samples.SubType>>(await _sampleTrackingApi.GetSubTypesAsync());
            viewModel.SampleAliquots = _mapper.Map<List<ApiModels.Samples.SampleAliquotApi>, List<SampleAliquot>>(await _sampleTrackingApi.GetAliquotsAsync(viewModel.Session.SessionId));
            viewModel.ScanHasExistingAliquots = false;



            foreach (var scan in scans)
            {
                var reScan = new Scan();
                var aliquot = _mapper.Map<ApiModels.Samples.SampleAliquotApi, Models.Samples.SampleAliquot>(await _sampleTrackingApi.GetAliquotAsync(scan));
                if (aliquot != null)
                {
                    reScan.ErrorMessage = "Aliquot already exists.";
                    reScan.ReScanSuccess = false;
                    viewModel.ReScanSuccess = false;
                    viewModel.ScanHasExistingAliquots = true;
                }

                var dupes = scans.Where(x => x == scan);
                if (dupes.Count() > 1)
                {
                    reScan.ErrorMessage = "Duplicate Aliquot.";
                    reScan.ReScanSuccess = false;
                    viewModel.ReScanSuccess = false;
                    viewModel.ScanHasExistingAliquots = true;
                }


                reScan.ScanId = scan;
                reScan.ReScanId = null;
                reScansList.Add(reScan);

            }

            viewModel.ScanList = reScansList;

            if (subTypeId != 0)
            {

            }

            return View(viewModel);
        }

        [HttpPost]
        [Route("ValidateSampleAliquots")]
        public async Task<IActionResult> ValidateSampleAliquotsPost(SelectSampleViewModel viewModel, string kbNumber, DateTime logDateTime, string patientName, int subTypeId, List<string> scans, List<Scan> scanList, bool fixScan, string save, string trayName)
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            viewModel.UserId = userId;
            var session = await _sampleTrackingApi.GetSessionAsync(userId);
            viewModel.Session = _mapper.Map<Session>(session);

            viewModel.Sample.KbNumber = kbNumber;
            viewModel.Sample.PatientName = patientName;
            viewModel.Sample.LogDateTime = logDateTime;
            viewModel.Tray.Description = trayName;
            viewModel.PageTitle = "Select Sub Type";
            viewModel.FixScan = fixScan;
            viewModel.SubTypeId = subTypeId;
            viewModel.SubTypes = _mapper.Map<List<ApiModels.Samples.SubTypeApi>, List<SubType>>(await _sampleTrackingApi.GetSubTypesAsync());
            viewModel.ReScanSuccess = true;
            viewModel.CompareScan = scanList.Select(s => s.ScanId).ToList();


            foreach (var scan in scanList)
            {
                scan.ReScanSuccess = true;

                var aliquot = _mapper.Map<ApiModels.Samples.SampleAliquotApi, SampleAliquot>(await _sampleTrackingApi.GetAliquotAsync(scan.ScanId));
                if (aliquot != null)
                {
                    scan.ErrorMessage = "Aliquot already exists.";
                    scan.ReScanSuccess = false;
                    viewModel.ReScanSuccess = false;

                }

                if (scan.ReScanId != null && !viewModel.CompareScan.Exists(s => s.Equals(scan.ReScanId)))
                {
                    scan.ErrorMessage = "Aliquot scan does not match initial scan.";
                    scan.ReScanSuccess = false;
                    viewModel.ReScanSuccess = false;
                }

                foreach (var duplicate in scanList.GroupBy(x => x.ReScanId).Where(g => g.Count() > 1))
                {
                    if (duplicate.Key != scan.ReScanId || duplicate.Key == null || scan.ReScanId == null)
                    {
                        continue;
                    }

                    scan.ErrorMessage = "Aliquot ID duplicated";
                    scan.ReScanSuccess = false;
                    viewModel.ReScanSuccess = false;
                }

                if (scan.ReScanId == null)
                {
                    scan.ReScanSuccess = false;
                    viewModel.ReScanSuccess = false;
                }
            }

            viewModel.ScanList = scanList;

            if (viewModel.ReScanSuccess && save == "Save")
            {
                await _sampleTrackingApi.AddSampleAliquot(viewModel);
                return RedirectToAction("SelectSample", "Samples");
            }
            else if (viewModel.ReScanSuccess && save == "Scan")
            {
                await _sampleTrackingApi.AddSampleAliquot(viewModel);
                await _sampleTrackingApi.UpdateSessionAsync(viewModel.UserId, 'A', session.SessionId);
                return RedirectToAction("Index", "Scans");
            }

            return View(viewModel);
        }
    }
}