using SampleTrackingUi.Models.Sessions;
using System.Collections.Generic;
using X.PagedList;

namespace SampleTrackingUi.ViewModels.Scans
{
    public class ScanTrayViewModel
    {
        public ScanTrayViewModel()
        {
            Session = new Session();
            FreezerId = 0;
            DrawerId = 0;
            DrawerSlotId = 0;
            MissingAliquots = new List<Models.Scans.TrayMissingAliquout>();
        }

        public string PageTitle { get; set; }
        public Session Session { get; set; }
        public int UserId { get; set; }
        public int FreezerId { get; set; }
        public int DrawerId { get; set; }
        public int DrawerSlotId { get; set; }
        public Models.Scans.Scan ScanData { get; set; }
        public IPagedList<Models.Scans.ScanDetail> ScanDetails { get; set; }
        public List<Models.Scans.TrayMissingAliquout> MissingAliquots { get; set; }
        public List<Models.Scans.TrayRelocateAliquots> RelocateAliquots { get; set; }
        public List<Models.Scans.TrayOrphanedAliquots> OrphanedAliquots { get; set; }
        public List<Models.Scans.TrayUnknownAliquot> UnknownAliquots { get; set; }
        public List<Models.Storage.Freezer> Freezers { get; set; }
        public List<Models.Storage.Drawer> Drawers { get; set; }
        public List<Models.Storage.DrawerSlot> DrawerSlots { get; set; }
        public List<Models.Scans.TrayMapSerum> TrayScanSerum { get; set; }
        public bool OverrideExceptions { get; set; }
        public string FreezerLocationVerifyDrawerId { get; set; }
        public string FreezerLocationVerifySlot { get; set; }
        public bool FreezerLocationValidate { get; set; }
        public bool FreezerLocationMatches { get; set; }
        public bool ShowFreezerLocationValidate { get; set; }
        public List<Models.Scans.ScanDetail> TrayData { get; set; }
    }
}
