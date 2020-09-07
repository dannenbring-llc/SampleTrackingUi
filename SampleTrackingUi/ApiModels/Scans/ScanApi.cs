using System;
using System.Collections.Generic;

namespace SampleTrackingUi.ApiModels.Scans
{
    public class ScanApi
    {
        public ScanApi()
        {
            ScanDetails = new List<ScanDetailApi>();
        }

        public int ScanId { get; set; }
        public int TrayId { get; set; }
        public string TrayCode { get; set; }
        public string TrayTypeDescription { get; set; }
        public DateTime CreateDateTime { get; set; }
        public List<ScanDetailApi> ScanDetails { get; set; }
    }

}
