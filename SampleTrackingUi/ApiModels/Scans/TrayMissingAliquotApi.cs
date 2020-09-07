namespace SampleTrackingUi.ApiModels.Scans
{
    public class TrayMissingAliquotApi
    {
        public int SessionId { get; set; }
        public int SampleAliquotId { get; set; }
        public char SaStatus { get; set; }
        public char SessionStatus { get; set; }
        public string AliquotId { get; set; }
    }
}
