using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleTrackingUi.ApiModels.Samples;
using SampleTrackingUi.ApiModels.Sessions;
using SampleTrackingUi.Models.Sessions;
using SampleTrackingUi.Services;
using SampleTrackingUi.ViewModels.Aliquots;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;

namespace SampleTrackingUi.Controllers
{
    [Route("Aliquots")]
    public class AliquotsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISampleTrackingApi _sampleTrackingApi;
        private readonly ILogger<AliquotsController> _logger;

        public AliquotsController(IMapper mapper, ISampleTrackingApi sampleTrackingApi, IIGTSamplesApi igtSamplesApi, ILogger<AliquotsController> logger)
        {
            _mapper = mapper;
            _sampleTrackingApi = sampleTrackingApi;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Move")]
        public async Task<IActionResult> MoveAliquots(MoveAliquotsViewModel vm)
        {
            //vm = new MoveTrayViewModel();
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var session = await _sampleTrackingApi.GetSessionAsync(userId);

            if (session != null)
            {
                vm.Session = _mapper.Map<Session>(session);
            }

            if (vm.Tray.Description != null && vm.Session.SessionId == 0)
            {
                //Create new Session
                var openSession = new OpenSessionApi
                {
                    UserId = userId,
                    TrayCode = vm.Tray.Description,
                    CloseExistingSession = true,
                    SessionTypeId = 1
                };

                var newSession = await _sampleTrackingApi.OpenSessionAsync(openSession);

                return RedirectToAction("Index", "Scans");
            }

            return View(vm);
        }


        [HttpGet("Delete")]
        public async Task<IActionResult> DeleteAliquots(MoveAliquotsViewModel vm)
        {
            //vm = new MoveTrayViewModel();
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var session = await _sampleTrackingApi.GetSessionAsync(userId);

            if (session != null)
            {
                vm.Session = _mapper.Map<Session>(session);
            }

            if (vm.Tray.Description == null)
            {
                return View(vm);
            }

            //Create new Session
            var openSession = new OpenSessionApi();
            //Create new Session
            openSession.UserId = userId;
            openSession.TrayCode = vm.Tray.Description;
            openSession.CloseExistingSession = true;
            openSession.SessionTypeId = 2;
            var newSession = await _sampleTrackingApi.OpenSessionAsync(openSession);
            vm.Session.SessionId = newSession.SessionId;
            vm.Session.UserId = newSession.UserId;
            vm.Tray.Description = newSession.TrayCode;
            vm.Session.TrayId = newSession.TrayId;
            vm.Tray.Id = newSession.TrayId;

            return RedirectToAction("DeleteAliquots", "Scans", new { vm.Session.TrayId });
        }


        [HttpGet("/Aliquot/Delete")]
        public async Task<IActionResult> DeleteAliquot(DeleteAliquotViewModel vm, string deleteAliquot)
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (deleteAliquot != "true")
            {
                return View(vm);
            }

            //Create new Session
            foreach (var aliquot in vm.AliquotsDelete)
            {
                if (aliquot != null)
                {

                    var deleteSampleAliquot = new SampleAliquotApi
                    {
                        AliquotId = aliquot.ToString(),
                        ModifydBy = userId
                    };

                    await _sampleTrackingApi.DeleteAliquotAsync(deleteSampleAliquot);
                }

            }

            return RedirectToAction("Index", "Home");
        }

    }
}