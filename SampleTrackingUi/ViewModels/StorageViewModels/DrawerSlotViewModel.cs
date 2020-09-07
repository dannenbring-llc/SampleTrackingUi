using SampleTrackingUi.Models.Storage;
using System.Collections.Generic;

namespace SampleTrackingUi.ViewModels.StorageViewModels
{
    public class DrawerSlotViewModel
    {
        public DrawerSlotViewModel()
        {
            DrawerSlots = new List<DrawerSlot>();
        }
        public Drawer Drawer { get; set; }
        public List<DrawerSlot> DrawerSlots { get; set; }
    }
}
