using FluentValidation;
using SampleTrackingUi.Models.Samples;
using SampleTrackingUi.Models.Sessions;
using SampleTrackingUi.Models.Storage;
using System.Collections.Generic;

namespace SampleTrackingUi.ViewModels.Samples
{
    public class SelectSampleViewModel
    {
        public SelectSampleViewModel()
        {
            Sample = new Sample();
            SubTypes = new List<SubType>();
            Scans = new List<string>();
            CompareScan = new List<string>();
            ReScans = new List<string>();
            ScanList = new List<Scan>();
            Freezers = new List<Freezer>();
            Drawers = new List<Drawer>();
            DrawerSlots = new List<DrawerSlot>();
            Session = new Session();
            Tray = new Tray();
        }

        public string PageTitle { get; set; }
        public Sample Sample { get; set; }
        public List<SubType> SubTypes { get; set; }
        public int SubTypeId { get; set; }
        public List<string> Scans { get; set; }
        public List<string> ReScans { get; set; }
        public List<Scan> ScanList { get; set; }
        public List<string> CompareScan { get; set; }
        public bool ReScanSuccess { get; set; }
        public bool ScanHasExistingAliquots { get; set; }
        public bool FixScan { get; set; }
        public List<Freezer> Freezers { get; set; }
        public List<Drawer> Drawers { get; set; }
        public List<DrawerSlot> DrawerSlots { get; set; }
        public int FreezerId { get; set; }
        public int DrawerId { get; set; }
        public int DrawerSlotId { get; set; }
        public string TrayId { get; set; }
        public Session Session { get; set; }
        public Tray Tray { get; set; }
        public int UserId { get; set; }
        public List<SampleAliquot> SampleAliquots { get; set; }
    }

    public class SelectSampleViewModelValidator : AbstractValidator<SelectSampleViewModel>
    {
        public SelectSampleViewModelValidator()
        {
            RuleFor(reg => reg.TrayId).NotEmpty();
        }
    }
}
