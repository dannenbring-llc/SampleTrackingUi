using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleTrackingUi.Models.Storage;
using SampleTrackingUi.Services;
using SampleTrackingUi.ViewModels.StorageViewModels;
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

        public StorageController(IMapper mapper, ISampleTrackingApi sampleTrackingApi, IIGTSamplesApi igtSamplesApi, ILogger<StorageController> logger)
        {
            _mapper = mapper;
            _sampleTrackingApi = sampleTrackingApi;
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

    }
}