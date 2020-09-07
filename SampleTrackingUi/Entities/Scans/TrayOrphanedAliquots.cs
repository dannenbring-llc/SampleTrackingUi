using System;
using System.Collections.Generic;
using System.Text;

namespace SampleTrackingUi.Models.Scans
{
    public class TrayOrphanedAliquots
    {
        public int AliquotLocationId { get; set; }
        public int TrayId { get; set; }
        public int TubeLocation { get; set; }
        public string TubeGridLocation { get; set; }
        public int UserCustodian { get; set; }
        public int SampleAliquotId { get; set; }
        public char StatusId { get; set; }
        public int AlTrayId { get; set; }
        public int AlSaid { get; set; }
        public int SessionId { get; set; }
    }
}
