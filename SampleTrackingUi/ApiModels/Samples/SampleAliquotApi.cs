using System;
using System.Runtime.Serialization;

namespace SampleTrackingUi.ApiModels.Samples
{
    public class SampleAliquotApi
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
