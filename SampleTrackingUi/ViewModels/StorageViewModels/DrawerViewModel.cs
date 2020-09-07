using SampleTrackingUi.Models.Storage;
using System.Collections.Generic;

namespace SampleTrackingUi.ViewModels.StorageViewModels
{
    public class DrawerViewModel
    {
        public DrawerViewModel()
        {
            Drawers = new List<Drawer>();
            Freezer = new Freezer();
        }
        public Freezer Freezer { get; set; }
        public List<Drawer> Drawers { get; set; }
    }
}
