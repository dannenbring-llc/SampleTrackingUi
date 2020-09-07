using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleTrackingUi.ApiModels.Samples;
using SampleTrackingUi.ApiModels.Sessions;
using SampleTrackingUi.Models.Scans;
using SampleTrackingUi.Models.Sessions;
using SampleTrackingUi.Models.Storage;
using SampleTrackingUi.Services;
using SampleTrackingUi.ViewModels.Scans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SampleTrackingUi.Controllers
{
    public class ScansController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISampleTrackingApi _sampleTrackingApi;
        private readonly IIGTSamplesApi _igtSamplesApi;
        private readonly ILogger _logger;

        public ScansController(
            IMapper mapper, 
            ISampleTrackingApi sampleTrackingApi, 
            IIGTSamplesApi igtSamplesApi, 
            ILogger<ScansController> logger
            )
        {
            _mapper = mapper;
            _sampleTrackingApi = sampleTrackingApi;
            _igtSamplesApi = igtSamplesApi;
            _logger = logger;
        }

        public async Task<IActionResult> Index(ScanTrayViewModel viewModel, int page, string closeSession, string validateFreezerLocation)
        {
            var pageNumber = page == 0 ? 1 : page;

            viewModel.UserId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            viewModel.Session = _mapper.Map<Session>(await _sampleTrackingApi.GetSessionAsync(viewModel.UserId));

            if (viewModel.Session != null)
            {
                viewModel.MissingAliquots = _mapper.Map<List<TrayMissingAliquout>>(await _sampleTrackingApi.GetMissingAliquotsAsync(viewModel.Session.SessionId));
                viewModel.OrphanedAliquots = _mapper.Map<List<TrayOrphanedAliquots>>(await _sampleTrackingApi.GetOrphanedAliquotsAsync(viewModel.Session.SessionId));
                viewModel.TrayScanSerum = _mapper.Map<List<TrayMapSerum>>(await _sampleTrackingApi.GetTrayMapSerumAsync(viewModel.Session.TrayId));
                viewModel.Freezers = _mapper.Map<List<Freezer>>(await _sampleTrackingApi.GetFreezersAsync()).OrderBy(f => f.Id).ToList();
                viewModel.Drawers = _mapper.Map<List<Drawer>>(await _sampleTrackingApi.GetDrawersAsync(viewModel.FreezerId)).OrderBy(d => d.Description).ToList();
                viewModel.DrawerSlots = _mapper.Map<List<DrawerSlot>>(await _sampleTrackingApi.GetDrawerSlotsAsync(viewModel.DrawerId)).OrderBy(ds => ds.Slot).ToList();
                viewModel.RelocateAliquots = _mapper.Map<List<TrayRelocateAliquots>>(await _sampleTrackingApi.GetRelocatedAliquotsAsync(viewModel.Session.SessionId));
                viewModel.UnknownAliquots = _mapper.Map<List<TrayUnknownAliquot>>(await _sampleTrackingApi.GetUnknownAliquotsAsync(viewModel.Session.SessionId));

                var scans = _mapper.Map<Models.Scans.Scan>(await _sampleTrackingApi.GetScanByTrayAsync(viewModel.Session.TrayId));

                //var scans = _mapper.Map<Models.Scans.Scan>(await _sampleTrackingApi.GetScanAsync(viewModel.UserId));
                viewModel.ScanData = scans;
                //viewModel.ScanDetails = scans.ScanDetails.ToPagedList(pageNumber, 15);
                //await _sampleTrackingApi.UpdateSessionAsync(viewModel.UserId, 'S', viewModel.Session.SessionId);

                if (viewModel.DrawerSlotId != 0)
                {
                    viewModel.ShowFreezerLocationValidate = true;
                }
                else
                {
                    viewModel.ShowFreezerLocationValidate = false;
                }

                viewModel.FreezerLocationValidate = validateFreezerLocation == "true";

                if (viewModel.FreezerLocationValidate)
                {
                    var test = viewModel.FreezerId;
                    var drawer = _mapper.Map<Drawer>(
                        await _sampleTrackingApi.GetDrawerByIdAsync(viewModel.DrawerId));
                    var drawerSlot =
                        _mapper.Map<DrawerSlot>(await _sampleTrackingApi.GetDrawerSlotByIdAsync(viewModel.DrawerSlotId));

                    if (drawer.Description.ToUpper() == viewModel.FreezerLocationVerifyDrawerId.ToUpper() && drawerSlot.Slot.Trim().ToUpper() == viewModel.FreezerLocationVerifySlot.Trim().ToUpper())
                    {
                        viewModel.FreezerLocationMatches = true;
                        closeSession = "true";
                    }
                    else
                    {
                        viewModel.FreezerLocationMatches = false;
                    }
                }

                if (closeSession == "true")
                {
                    var closeSessionVm = new CloseSessionApi
                    {
                        SessionId = viewModel.Session.SessionId,
                        UserId = viewModel.UserId,
                        DrawerSlot = viewModel.DrawerSlotId,
                        CloseDate = DateTime.Now
                    };

                    var closeSessionResults = await _sampleTrackingApi.CloseSession(closeSessionVm);

                    _logger.Log(LogLevel.Debug, $"Close Session: Freezer - {viewModel.FreezerId}, Drawer - {viewModel.DrawerId}, Drawer Slot - {viewModel.DrawerSlotId}");
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(viewModel);
        }

        public async Task<IActionResult> DeleteAliquots(ScanTrayViewModel viewModel, int page, string closeSession, string deleteAliquots, int trayId)
        {
            var pageNumber = page == 0 ? 1 : page;

            viewModel.UserId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            viewModel.Session = _mapper.Map<Session>(await _sampleTrackingApi.GetSessionAsync(viewModel.UserId));

            if (viewModel.Session != null)
            {
                viewModel.MissingAliquots = _mapper.Map<List<TrayMissingAliquout>>(await _sampleTrackingApi.GetMissingAliquotsAsync(viewModel.Session.SessionId));
                viewModel.OrphanedAliquots = _mapper.Map<List<TrayOrphanedAliquots>>(await _sampleTrackingApi.GetOrphanedAliquotsAsync(viewModel.Session.SessionId));
                viewModel.TrayScanSerum = _mapper.Map<List<TrayMapSerum>>(await _sampleTrackingApi.GetTrayMapSerumAsync(viewModel.Session.TrayId));
                viewModel.Freezers = _mapper.Map<List<Freezer>>(await _sampleTrackingApi.GetFreezersAsync()).OrderBy(f => f.Id).ToList();
                viewModel.Drawers = _mapper.Map<List<Drawer>>(await _sampleTrackingApi.GetDrawersAsync(viewModel.FreezerId)).OrderBy(d => d.Description).ToList();
                viewModel.DrawerSlots = _mapper.Map<List<DrawerSlot>>(await _sampleTrackingApi.GetDrawerSlotsAsync(viewModel.DrawerId)).OrderBy(ds => ds.Slot).ToList();
                viewModel.RelocateAliquots = _mapper.Map<List<TrayRelocateAliquots>>(await _sampleTrackingApi.GetRelocatedAliquotsAsync(viewModel.Session.SessionId));
                viewModel.UnknownAliquots = _mapper.Map<List<TrayUnknownAliquot>>(await _sampleTrackingApi.GetUnknownAliquotsAsync(viewModel.Session.SessionId));

                //var scans = _mapper.Map<Models.Scans.Scan>(await _sampleTrackingApi.GetScanAsync(trayId));
                var scans = _mapper.Map<Models.Scans.Scan>(await _sampleTrackingApi.GetScanByTrayAsync(viewModel.Session.TrayId));

                viewModel.ScanData = scans;
                viewModel.TrayData = scans.ScanDetails.Where(sd => sd.SampleId != null).ToList();
                //viewModel.ScanDetails = scans.ScanDetails.ToPagedList(pageNumber, 15);
                //await _sampleTrackingApi.UpdateSessionAsync(viewModel.UserId, 'S', viewModel.Session.SessionId);

                if (viewModel.DrawerSlotId != 0)
                {
                    viewModel.ShowFreezerLocationValidate = true;
                }
                else
                {
                    viewModel.ShowFreezerLocationValidate = false;
                }

                if (closeSession == "true")
                {
                    var closeSessionVm = new CloseSessionApi
                    {
                        SessionId = viewModel.Session.SessionId,
                        UserId = viewModel.UserId,
                        DrawerSlot = viewModel.DrawerSlotId,
                        CloseDate = DateTime.Now
                    };


                    var deleteTray = new TrayApi();
                    deleteTray.Id = viewModel.Session.TrayId;
                    deleteTray.UserId = viewModel.UserId;
                    await _sampleTrackingApi.DeleteTrayAsync(deleteTray);
                    var closeSessionResults = await _sampleTrackingApi.CloseSessionDelete(closeSessionVm);

                    _logger.Log(LogLevel.Debug, $"Close Session: Freezer - {viewModel.FreezerId}, Drawer - {viewModel.DrawerId}, Drawer Slot - {viewModel.DrawerSlotId}");
                    return RedirectToAction("Index", "Home");
                }

                if (deleteAliquots == "true")
                {
                    var deleteTrayData = new TrayApi();
                    deleteTrayData.Id = viewModel.Session.TrayId;
                    deleteTrayData.UserId = viewModel.UserId;
                    var results = _sampleTrackingApi.DeleteTrayDataAsync(deleteTrayData);

                    var closeSessionVm = new CloseSessionApi
                    {
                        SessionId = viewModel.Session.SessionId,
                        UserId = viewModel.UserId,
                        DrawerSlot = viewModel.DrawerSlotId,
                        CloseDate = DateTime.Now
                    };

                    var closeSessionResults = await _sampleTrackingApi.CloseSessionDelete(closeSessionVm);

                    _logger.Log(LogLevel.Debug, $"Close Session: Freezer - {viewModel.FreezerId}, Drawer - {viewModel.DrawerId}, Drawer Slot - {viewModel.DrawerSlotId}");

                    return RedirectToAction("Index", "Home");

                }
            }

            return View(viewModel);
        }

    }
}