using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleTrackingUi.ApiModels.Samples
{
    public class AliquotInformationApi
    {
        public string AliquotId { get; set; }
        public string KbNumber { get; set; }
        public DateTime LogDate { get; set; }
        public string PatientId { get; set; }
        public string PatientName { get; set; }
        public DateTime SampleDate { get; set; }
        public int TrayId { get; set; }
        public string TrayDescription { get; set; }
        public string TubeGridLocation { get; set; }
        public int UserCustodian { get; set; }
        public int DrawerSlotId { get; set; }
        public int DrawerId { get; set; }
        public string DrawerDescription { get; set; }
        public int FreezerId { get; set; }
        public string FreezerDescription { get; set; }
    }
}
