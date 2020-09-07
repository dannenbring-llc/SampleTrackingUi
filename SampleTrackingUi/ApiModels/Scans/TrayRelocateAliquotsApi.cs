namespace SampleTrackingUi.ApiModels.Scans
{
    public class TrayRelocateAliquotsApi
    {
        public int SessionId { get; set; }
        public char SessionStatus { get; set; }
        public int SampleAliquotId { get; set; }
        public int AliquotLocationId { get; set; }
        public int TrayId { get; set; }
        public int TubeLocation { get; set; }
        public string TubeGridLocation { get; set; }
        public char StatusId { get; set; }
    }
}
