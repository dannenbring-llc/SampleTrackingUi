using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleTrackingUi.Models.Scans
{
    public class TrayTabular
    {
        public TrayTabular(string trayDescription, string sampleId, string aliquotId, string patientId, string trayLocation)
        {
            TrayDescription = trayDescription;
            SampleId = sampleId;
            AliquotId = aliquotId;
            PatientId = patientId;
            TrayLocation = trayLocation;
        }
        //t.TrayDescription, t.SampleId, t.AliquotId, t.PatientName, t.TrayLocation
        public string TrayDescription { get; set; }
        public string SampleId { get; set; }
        public string AliquotId { get; set; }
        public string PatientId { get; set; }
        public string TrayLocation { get; set; }
    }
}
