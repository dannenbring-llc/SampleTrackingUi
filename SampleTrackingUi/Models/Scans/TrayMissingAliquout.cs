using System;
using System.Collections.Generic;
using System.Text;

namespace SampleTrackingUi.Models.Scans
{
    public class TrayMissingAliquout
    {
        public int SessionId { get; set; }
        public int SampleAliquotId { get; set; }
        public char SAStatus { get; set; }
        public char SessionStatus { get; set; }
        public string AliquotId { get; set; }
    }
}
