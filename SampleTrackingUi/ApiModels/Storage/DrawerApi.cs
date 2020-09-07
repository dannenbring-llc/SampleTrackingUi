using System;
using System.Collections.Generic;

namespace SampleTrackingUi.ApiModels.Storage
{
    public class DrawerApi
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public int FreezerId { get; set; }
        public char Status { get; set; }
        public int DrawerCapacity { get; set; }
        public int AliquotCount { get; set; }
        public float PercentFull { get; set; }

        public int CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public int ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }
        public List<DrawerSlotApi> DrawerSlots { get; set; }

    }
}
