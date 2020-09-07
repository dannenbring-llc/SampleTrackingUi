using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleTrackingUi.Models.Samples
{
    public class Tray
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int TrayCapacity { get; set; }
        public char StatusId { get; set; }
        public int TrayTypeId { get; set; }
        public int SessionId { get; set; }
    }
}