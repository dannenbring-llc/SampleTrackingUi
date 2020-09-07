using SampleTrackingUi.ApiModels.Samples;
using System;

namespace SampleTrackingUi.ApiModels.Storage
{
    public class DrawerSlotApi
    {
        public int Id { get; set; }
        public int DrawerId { get; set; }
        public string Slot { get; set; }
        public char Status { get; set; }
        public int DrawerSlotCapacity { get; set; }
        public int AliquotCount { get; set; }
        public float PercentFull { get; set; }
        public int CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public int ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }
        public TrayApi Tray { get; set; }

    }
}
