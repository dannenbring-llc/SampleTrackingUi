namespace SampleTrackingUi.Models.Sessions
{
    public class OpenSession
    {
        public string TrayCode { get; set; }
        public bool CloseExistingSession { get; set; }
        public int SessionTypeId { get; set; }
        public int UserId { get; set; }
        public int TrayId { get; set; }
        public int SessionId { get; set; }
        public string OutputMessage { get; set; }
        public int Output { get; set; }
    }
}
