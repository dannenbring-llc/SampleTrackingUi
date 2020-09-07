using System.Runtime.Serialization;

namespace SampleTrackingUi.ApiModels.Samples
{
    public class TrayApi
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int TrayCapacity { get; set; }
        public char StatusId { get; set; }
        public int TrayTypeId { get; set; }
        public int SessionId { get; set; }
        public int UserId { get; set; }
    }
}
