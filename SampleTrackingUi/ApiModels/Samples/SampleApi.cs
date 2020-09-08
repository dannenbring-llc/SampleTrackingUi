using System;
using System.Runtime.Serialization;

namespace SampleTrackingUi.ApiModels.Samples
{
    public class SampleApi
    {
        public string KbNumber { get; set; }
        public string PatientId { get; set; }
        public string PatientName { get; set; }
        public DateTime LogDateTime { get; set; }
        public DateTime SampleDate { get; set; }
        public string SampleDateString { get
            {
                return SampleDate.ToShortDateString();
            }
        }
    }
}
