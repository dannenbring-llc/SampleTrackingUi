using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SampleTrackingUi.Models.Storage;
using SampleTrackingUi.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleTrackingUi.Controllers
{
    [Route("api/Support")]
    [ApiController]
    public class SupportController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISampleTrackingApi _sampleTrackingApi;
        private readonly IIGTSamplesApi _igtSamplesApi;

        public SupportController(IMapper mapper, ISampleTrackingApi sampleTrackingApi, IIGTSamplesApi igtSamplesApi)
        {
            _mapper = mapper;
            _sampleTrackingApi = sampleTrackingApi;
            _igtSamplesApi = igtSamplesApi;
        }

        [HttpGet]
        [Route("DrawersFree/{freezerId}")]
        public async Task<IActionResult> GetDrawers(int freezerId)
        {
            var drawers = _mapper.Map<List<Drawer>>(await _sampleTrackingApi.GetDrawersFreeAsync(freezerId)).OrderBy(d => d.Description);
            if (drawers == null)
            {
                return NotFound();
            }

            return Ok(drawers);
        }

        [HttpGet]
        [Route("DrawerSlotsFree/{drawerId}/{trayDescription}")]
        public async Task<IActionResult> GetDrawerSlots(int drawerId, string trayDescription)
        {
            var drawerSlots = _mapper.Map<List<DrawerSlot>>(await _sampleTrackingApi.GetDrawerSlotsFreeAsync(drawerId, trayDescription)).OrderBy(ds => ds.Slot);
            if (drawerSlots == null)
            {
                return NotFound();
            }

            return Ok(drawerSlots);
        }

        [HttpGet]
        [Route("PatientAutoComplete/{prefix}")]
        public async Task<IActionResult> GetPatients(string prefix)
        {
            var drawerSlots = _mapper.Map<List<DrawerSlot>>(await _igtSamplesApi.GetPatientsAsync(prefix)).OrderBy(ds => ds.Slot);
            if (drawerSlots == null)
            {
                return NotFound();
            }

            return Ok(drawerSlots);
        }

    }
}