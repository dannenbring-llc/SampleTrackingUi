using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleTrackingUi.Models.Samples
{
    public class Sample
    {
        public string KbNumber { get; set; }
        public string PatientName { get; set; }
        public DateTime LogDateTime { get; set; }
    }
}
