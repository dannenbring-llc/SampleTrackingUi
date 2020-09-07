using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleTrackingUi.Models.Scans
{
    public class TrayColumn
    {
        public string Sample { get; set; }
        public string Aliquot { get; set; }
        public bool IsUnknownAliquot { get; set; }
        public string UnknownAliquotId { get; set; }
        public bool IsRelocatedAliquot { get; set; }
        public string RelocatedAliquotId { get; set; }
        public bool FindSampleId { get; set; }
    }
}
