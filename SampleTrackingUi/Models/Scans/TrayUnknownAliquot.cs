using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleTrackingUi.Models.Scans
{
    public class TrayUnknownAliquot
    {
        public int ScanId { get; set; }
        public string TrayCode { get; set; }
        public int ScanDetailId { get; set; }
        public string AliquotId { get; set; }
        public int PositionNumber { get; set; }
        public string PositionText { get; set; }
    }
}
