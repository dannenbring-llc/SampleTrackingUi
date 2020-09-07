using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleTrackingUi.ApiModels.Scans
{
    public class SearchResultBySampleApi
    {
        public SearchResultBySampleApi()
        {
            Trays = new List<TrayMapSerumApi>();
        }

        public int TrayId { get; set; }
        public string TrayDescription { get; set; }
        public int FreezerId {get; set; }
        public string FreezerDescription { get; set; }
        public string DrawerDescription { get; set; }
        public string DrawerSlot { get; set; }
        public List<TrayMapSerumApi> Trays { get; set; }

    }
}
