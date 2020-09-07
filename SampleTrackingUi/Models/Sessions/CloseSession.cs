using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleTrackingUi.Models.Sessions
{
    public class CloseSession
    {
        public int SessionId { get; set; }
        public int UserId { get; set; }
        public int DrawerSlot { get; set; }
        public bool Override { get; set; }
        public DateTime CloseDate { get; set; }
        public int Missing { get; set; }
        public int Orphan { get; set; }
        public int Relocate { get; set; }
        public int DrawerSlotOccupiedTrayId { get; set; }
        public string DrawerSlotOccupiedTrayCode { get; set; }
        public string OutputMessage { get; set; }
        public string DrawerSlotAction { get; set; }
        public int Output { get; set; }
    }
}
