using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleTrackingUi.Models.Samples
{
    public class SampleAliquot
    {
        public int SampleAliquotId { get; set; }
        public string SampleId { get; set; }
        public string AliquotId { get; set; }
        public int SubTypeId { get; set; }
        public string StatusId { get; set; }
        public int SessionId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifydBy { get; set; }
        public DateTime ModifyDateTime { get; set; }
    }
}
