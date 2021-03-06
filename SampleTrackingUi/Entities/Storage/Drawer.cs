﻿using System;
using System.Collections.Generic;

namespace SampleTrackingUi.Models.Storage
{
    public class Drawer
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public int FreezerId { get; set; }
        public char Status { get; set; }
        public int DrawerCapacity { get; set; }
        public int AliquotCount { get; set; }
        public float PercentFull { get; set; }
        public float PercentFree => 100 - PercentFull;
        public int CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public int ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }
        public List<DrawerSlot> DrawerSlots { get; set; }

    }
}
