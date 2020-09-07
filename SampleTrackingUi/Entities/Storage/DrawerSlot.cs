using SampleTrackingUi.Models.Samples;
using System;

namespace SampleTrackingUi.Models.Storage
{
    public class DrawerSlot
    {
        public int Id { get; set; }
        public int DrawerId { get; set; }
        public string Slot { get; set; }
        public char Status { get; set; }
        public int DrawerSlotCapacity { get; set; }
        public int AliquotCount { get; set; }
        public float PercentFull { get; set; }
        public float PercentFree => 100 - PercentFull;
        public int CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public int ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }
        public Tray Tray { get; set; }

    }
}
