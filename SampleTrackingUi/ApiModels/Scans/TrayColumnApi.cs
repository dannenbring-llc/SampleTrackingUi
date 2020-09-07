using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleTrackingUi.ApiModels.Scans
{
    public class TrayColumnApi
    {
        public string Sample { get; set; }
        public DateTime LogDate { get; set; }
        public string Aliquot { get; set; }
        public bool IsUnknownAliquot { get; set; }
        public string UnknownAliquotId { get; set; }
        public bool IsRelocatedAliquot { get; set; }
        public string RelocatedAliquotId { get; set; }
        public bool FindSampleId { get; set; }
    }
}
