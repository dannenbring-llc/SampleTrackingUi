using System;

namespace SampleTrackingUi.ApiModels.Storage
{
    public class TrayDataApi
    {
        public int TrayId { get; set; }
        public string TrayDescription { get; set; }
        public int TrayCapacity { get; set; }
        public string StatusId { get; set; }
        public int TrayTypeId { get; set; }
        public int SessionId { get; set; }
        public string TrayLocation { get; set; }
        public int SampleAliquotId { get; set; }
        public string AliquotId { get; set; }
        public string SampleId { get; set; }
        public DateTime LogDate { get; set; }
        public string PatientId { get; set; }
        public string PatientName { get; set; }
        public DateTime SampleDate { get; set; }
    }
}
