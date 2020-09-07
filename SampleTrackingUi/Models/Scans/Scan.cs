using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleTrackingUi.Models.Scans
{
    public class Scan
    {
        public Scan()
        {
            ScanDetails = new List<ScanDetail>();
        }
        public int ScanId { get; set; }
        public int TrayId { get; set; }
        public string TrayCode { get; set; }
        public string TrayTypeDescription { get; set; }
        public DateTime CreateDateTime { get; set; }
        public List<ScanDetail> ScanDetails { get; set; }
    }
}
