using System;

namespace SampleTrackingUi.ApiModels.Scans
{
    public class TrayMapDataApi
    {
        public int TrayId { get; set; }
        public string TrayDescription { get; set; }
        public int TrayCapacity { get; set; }
        public string TrayStatus { get; set; }
        public int TrayTypeId { get; set; }
        public int SessionId { get; set; }
        public int CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public int ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }
        public string TrayLocation { get; set; }
        public string SampleAliquotId { get; set; }
        public string TrayRow { get; set; }
        public string TrayCol { get; set; }
        public string AliquotId { get; set; }
        public string SampleId { get; set; }
        public bool UnknownAliquot { get; set; }
        public string RelocatedAliquotId { get; set; }
        public DateTime LogDate { get; set; }
        public string PatientName { get; set; }
        public string PatientId { get; set; }
    }
}
