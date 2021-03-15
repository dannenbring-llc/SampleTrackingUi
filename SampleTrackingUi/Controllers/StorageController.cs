using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleTrackingUi.ApiModels.Storage;
using SampleTrackingUi.Entities.Storage;
using SampleTrackingUi.Models.Storage;
using SampleTrackingUi.Services;
using SampleTrackingUi.ViewModels.StorageViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleTrackingUi.Controllers
{
    [Route("Storage")]
    public class StorageController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISampleTrackingApi _sampleTrackingApi;
        private readonly ILogger<StorageController> _logger;
        private readonly IIGTSamplesApi _igtSamplesApi;


        public StorageController(IMapper mapper, ISampleTrackingApi sampleTrackingApi, IIGTSamplesApi igtSamplesApi, ILogger<StorageController> logger)
        {
            _mapper = mapper;
            _sampleTrackingApi = sampleTrackingApi;
            _igtSamplesApi = igtSamplesApi;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Drawers/{freezerId}")]
        public async Task<IActionResult> Drawers(DrawerViewModel vm, int freezerId)
        {
            vm = new DrawerViewModel();
            vm.Freezer = _mapper.Map<Freezer>(await _sampleTrackingApi.GetFreezerAsync(freezerId));
            vm.Freezer.Drawers = vm.Freezer.Drawers.OrderBy(d => d.Description).ToList();
            //vm.Drawers = _mapper.Map<List<Drawer>>(await _sampleTrackingApi.GetFreezerAsync(freezerId));
            return View(vm);
        }

        [HttpGet("DrawerSlots/{freezerId}/{drawerId}")]
        public async Task<IActionResult> DrawerSlots(DrawerSlotViewModel vm, int freezerId, int drawerId)
        {
            vm = new DrawerSlotViewModel();
            vm.Drawer = _mapper.Map<Drawer>(await _sampleTrackingApi.GetDrawerAsync(freezerId, drawerId));
            //vm.DrawerSlots = _mapper.Map<List<DrawerSlot>>(await _sampleTrackingApi.GetDrawerAsync(freezerId, drawerId));
            return View(vm);
        }

        [HttpGet("RackAssignment")]
        public async Task<IActionResult> RackAssignment(RackAssignmentViewModel vm)
        {
            vm.Racks = _mapper.Map<List<Rack>>(await _sampleTrackingApi.GetRacks());

            if (vm.RackId == null)
            {
                return View(vm);
            }

            if (vm.SampleRackLocations != null)
            {

                for (int i = 0; i < vm.SampleRackLocations.Count; i++)
                {
                    if (!string.IsNullOrEmpty(vm.SampleRackLocations[i].SampleId))
                    {
                        var sample = await _igtSamplesApi.GetSampleAsync(vm.SampleRackLocations[i].SampleId);
                        if (string.IsNullOrEmpty(sample.KbNumber))
                        {
                            vm.ShowSaveButton = false;
                            return View(vm);
                        }
                        //viewModel.Labels[i].LogNumber = sample.KbNumber;
                        //viewModel.Labels[i].PatId = sample.PatientId;
                        //viewModel.Labels[i].PatientName = sample.PatientName;
                        vm.ShowSaveButton= true;
                    }
                }
                if (vm.SampleRackLocations.Where(sr => string.IsNullOrEmpty(sr.SampleId)).Count() == 0)
                {
                    vm.SampleRackLocations.Add(new SampleRackLocationApi());
                }
            }

            return View(vm);
        }

        [HttpPost("RackAssignment")]
        public async Task<IActionResult> RackAssignmentPost(RackAssignmentViewModel vm)
        {
            var results = vm.SampleRackLocations.Where(r => r.SampleId != null).Select(r => r.SampleId);
            foreach (var item in results)
            {
                await _sampleTrackingApi.UpdateSampleRackAsync(item, vm.RackId);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}