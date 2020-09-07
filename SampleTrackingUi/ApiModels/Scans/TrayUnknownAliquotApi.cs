namespace SampleTrackingUi.ApiModels.Scans
{
    public class TrayUnknownAliquotApi
    {
        public int ScanId { get; set; }
        public string TrayCode { get; set; }
        public int ScanDetailId { get; set; }
        public string AliquotId { get; set; }
        public int PositionNumber { get; set; }
        public string PositionText { get; set; }
    }
}
