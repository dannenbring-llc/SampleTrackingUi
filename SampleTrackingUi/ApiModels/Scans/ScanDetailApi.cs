using System;

namespace SampleTrackingUi.ApiModels.Scans
{
    public class ScanDetailApi
    {
        public int ScanDetailId { get; set; }
        public int ScanId { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public int PositionNumber { get; set; }
        public string PositionText { get; set; }
        public string AliquotId { get; set; }
        public string ScannerType { get; set; }
        public string SampleId { get; set; }
        public DateTime LogDate { get; set; }
        public string PatientId { get; set; }
        public string PatientName { get; set; }
        public DateTime SampleDate { get; set; }

    }
}
