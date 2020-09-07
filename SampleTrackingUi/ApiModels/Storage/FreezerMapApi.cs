using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleTrackingUi.ApiModels.Storage
{
    public class FreezerMapApi
    {
        public FreezerMapApi()
        {
            Freezers = new List<FreezerApi>();
        }
        public List<FreezerApi> Freezers { get; set; }
    }
}
