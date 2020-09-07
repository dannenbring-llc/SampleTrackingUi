using SampleTrackingUi.Models.Sessions;
using SampleTrackingUi.Models.Storage;
using System.Collections.Generic;

namespace SampleTrackingUi.ViewModels.HomeViewModels
{
    public class HomeViewModel
    {
        public Session Session { get; set; }
        public int FreezerId { get; set; }
        public string DrawerDescription { get; set; }
        public string DrawerSlot { get; set; }
        public FreezerMap FreezerMap { get; set; }
        public List<Freezer> Freezers { get; set; }
    }
}
