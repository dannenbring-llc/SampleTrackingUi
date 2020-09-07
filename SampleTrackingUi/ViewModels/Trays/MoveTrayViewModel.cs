using SampleTrackingUi.Models.Samples;
using SampleTrackingUi.Models.Sessions;
using SampleTrackingUi.Models.Storage;
using System.Collections.Generic;

namespace SampleTrackingUi.ViewModels.Trays
{
    public class MoveTrayViewModel
    {
        public MoveTrayViewModel()
        {
            Session = new Session();
            Tray = new Tray();
            CurrentTrayLocation = new TrayLocation();
            FreezerId = 0;
            DrawerId = 0;
            DrawerSlotId = 0;
        }

        public Session Session { get; set; }
        public TrayLocation CurrentTrayLocation { get; set; }
        public Tray Tray { get; set; }
        public bool TrayExists { get; set; }
        public bool CreateTray { get; set; }
        public int FreezerId { get; set; }
        public int DrawerId { get; set; }
        public int DrawerSlotId { get; set; }
        public string ErrorMessage { get; set; }
        public List<Models.Storage.Freezer> Freezers { get; set; }
        public List<Models.Storage.Drawer> Drawers { get; set; }
        public List<Models.Storage.DrawerSlot> DrawerSlots { get; set; }

    }
}
