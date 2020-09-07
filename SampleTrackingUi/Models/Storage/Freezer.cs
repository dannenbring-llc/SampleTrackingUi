using System.Collections.Generic;

namespace SampleTrackingUi.Models.Storage
{
    public class Freezer
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public char Status { get; set; }
        public int FreezerCapacity { get; set; }
        public int AliquotCount { get; set; }
        public float PercentFull { get; set; }
        public float PercentFree => 100 - PercentFull;

        public List<Drawer> Drawers { get; set; }

    }
}
