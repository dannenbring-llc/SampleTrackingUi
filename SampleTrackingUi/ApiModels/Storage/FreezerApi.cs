using System.Collections.Generic;

namespace SampleTrackingUi.ApiModels.Storage
{
    public class FreezerApi
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public char Status { get; set; }
        public int FreezerCapacity { get; set; }
        public int AliquotCount { get; set; }
        public float PercentFull { get; set; }
        public List<DrawerApi> Drawers { get; set; }
    }
}
