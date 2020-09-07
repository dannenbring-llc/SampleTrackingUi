using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleTrackingUi.Models.Storage
{
    public class FreezerMap
    {
        public FreezerMap()
        {
            Freezers = new List<Freezer>();
        }
        public List<Freezer> Freezers { get; set; }
    }
}
