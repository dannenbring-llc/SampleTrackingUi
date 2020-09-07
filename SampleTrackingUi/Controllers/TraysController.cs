using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleTrackingUi.ApiModels.Samples;
using SampleTrackingUi.ApiModels.Storage;
using SampleTrackingUi.Models.Samples;
using SampleTrackingUi.Models.Sessions;
using SampleTrackingUi.Models.Storage;
using SampleTrackingUi.Services;
using SampleTrackingUi.ViewModels.Trays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SampleTrackingUi.Controllers
{
    public class TraysController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISampleTrackingApi _sampleTrackingApi;
        private readonly ILogger<TraysController> _logger;

        public TraysController(IMapper mapper, ISampleTrackingApi sampleTrackingApi, IIGTSamplesApi igtSamplesApi, ILogger<TraysController> logger)
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
        public async Task<IActionResult> MoveTray(MoveTrayViewModel vm, string saveTrayRelocation)
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var session = await _sampleTrackingApi.GetSessionAsync(userId);

            if (session != null)
            {
                vm.Session = _mapper.Map<Session>(session);
            }

            if (vm.Tray.Description != null)
            {
                var tray = _mapper.Map<Tray>(await _sampleTrackingApi.GetTrayAsync(vm.Tray.Description));
                vm.TrayExists = tray != null;
                if (!vm.TrayExists)
                {
                    vm.ErrorMessage = "Tray does not exist.";
                }
                else
                {
                    vm.CurrentTrayLocation = _mapper.Map<TrayLocation>(await _sampleTrackingApi.GetTrayLocationAsync(tray.Id));

                    vm.Freezers = _mapper.Map<List<Freezer>>(await _sampleTrackingApi.GetFreezersAsync()).OrderBy(f => f.Id).ToList();
                    vm.Drawers = _mapper.Map<List<Drawer>>(await _sampleTrackingApi.GetDrawersAsync(vm.FreezerId)).OrderBy(d => d.Description).ToList();
                    vm.DrawerSlots = _mapper.Map<List<DrawerSlot>>(await _sampleTrackingApi.GetDrawerSlotsAsync(vm.DrawerId)).OrderBy(ds => ds.Slot).ToList();
                }
            }

            if (saveTrayRelocation == "true")
            {
                var newTrayLocation = new TrayLocationApi();
                newTrayLocation.TrayLocationId = vm.CurrentTrayLocation.TrayLocationId;
                newTrayLocation.FreezerId = vm.FreezerId;
                newTrayLocation.DrawerId = vm.DrawerId;
                newTrayLocation.DrawerSlotId = vm.DrawerSlotId;
                newTrayLocation.TrayId = vm.CurrentTrayLocation.TrayId;
                newTrayLocation.UserId = userId;
                var results = _sampleTrackingApi.UpdateTrayLocationAsync(newTrayLocation);
                return RedirectToAction("Index", "Home");
            }
            //vm.Freezer = _mapper.Map<Freezer>(await _sampleTrackingApi.GetFreezerAsync(freezerId));
            //vm.Drawers = _mapper.Map<List<Drawer>>(await _sampleTrackingApi.GetFreezerAsync(freezerId));
            return View(vm);
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> DeleteTray(DeleteTrayViewModel vm, string deleteTray)
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var session = await _sampleTrackingApi.GetSessionAsync(userId);

            if (session != null)
            {
                vm.Session = _mapper.Map<Session>(session);
            }
            //else
            //{
            //    var openSession = new OpenSessionApi
            //    {
            //        UserId = userId,
            //        TrayCode = vm.Tray.Description,
            //        CloseExistingSession = true
            //    };
            //    //Create new Session
            //    var newSession = await _sampleTrackingApi.OpenSessionAsync(openSession);
            //    vm.Session.SessionId = newSession.SessionId;
            //    vm.Session.UserId = newSession.UserId;
            //    vm.Tray.Description = newSession.TrayCode;
            //    vm.Tray.Id = newSession.TrayId;
            //}

            if (vm.Tray.Description != null)
            {
                var tray = _mapper.Map<Tray>(await _sampleTrackingApi.GetTrayAsync(vm.Tray.Description));
                vm.TrayExists = tray != null;
                if (!vm.TrayExists)
                {
                    vm.ErrorMessage = "Tray does not exist.";
                }
                else
                {
                    vm.CurrentTrayLocation = _mapper.Map<TrayLocation>(await _sampleTrackingApi.GetTrayLocationAsync(tray.Id));

                    vm.Freezers = _mapper.Map<List<Freezer>>(await _sampleTrackingApi.GetFreezersAsync()).OrderBy(f => f.Id).ToList();
                    vm.Drawers = _mapper.Map<List<Drawer>>(await _sampleTrackingApi.GetDrawersAsync(vm.FreezerId)).OrderBy(d => d.Description).ToList();
                    vm.DrawerSlots = _mapper.Map<List<DrawerSlot>>(await _sampleTrackingApi.GetDrawerSlotsAsync(vm.DrawerId)).OrderBy(ds => ds.Slot).ToList();
                    vm.TrayData = _mapper.Map<List<TrayData>>(await _sampleTrackingApi.GetTrayDataAsync(vm.Tray.Description)).OrderBy(td => td.TrayLocation).ToList();
                }
            }

            if (deleteTray == "true")
            {
                var deleteTrayData = new TrayApi();
                deleteTrayData.Id = vm.CurrentTrayLocation.TrayId;
                deleteTrayData.UserId = userId;
                var results = _sampleTrackingApi.DeleteTrayDataAsync(deleteTrayData);
                return RedirectToAction("Index", "Home", new { deleteAliquots = true });
            }
            //vm.Freezer = _mapper.Map<Freezer>(await _sampleTrackingApi.GetFreezerAsync(freezerId));
            //vm.Drawers = _mapper.Map<List<Drawer>>(await _sampleTrackingApi.GetFreezerAsync(freezerId));
            return View(vm);
        }

    }
}