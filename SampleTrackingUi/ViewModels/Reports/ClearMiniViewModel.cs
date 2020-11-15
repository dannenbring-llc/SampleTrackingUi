using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleTrackingUi.ViewModels.Reports
{
    public class ClearMiniViewModel
    {
        public string LogNumber { get; set; }
        public string PatId { get; set; }
        public string PatientName { get; set; }
        public bool ShowReportButton { get; set; }
    }
}
