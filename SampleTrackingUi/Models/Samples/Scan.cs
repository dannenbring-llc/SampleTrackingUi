namespace SampleTrackingUi.Models.Samples
{
    public class Scan
    {
        public string SampleId { get; set; }
        public string ScanId { get; set; }
        public string ReScanId { get; set; }
        public string ThirdScan { get; set; }
        public bool ReScanSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }

}
