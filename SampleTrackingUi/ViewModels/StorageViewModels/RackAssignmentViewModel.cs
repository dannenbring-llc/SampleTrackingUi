using SampleTrackingUi.ApiModels.Storage;
using SampleTrackingUi.Entities.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleTrackingUi.ViewModels.StorageViewModels
{
    public class RackAssignmentViewModel
    {
        public RackAssignmentViewModel()
        {
            Racks = new List<Rack>();
            SampleRackLocations = new List<SampleRackLocationApi>();
            var sampleRack = new SampleRackLocationApi();
            SampleRackLocations.Add(sampleRack);
        }

        public List<Rack> Racks { get; set; }
        public List<SampleRackLocationApi> SampleRackLocations { get; set; }
        public bool ShowSaveButton { get; set; }
        public string RackId { get; set; }
    }
}
