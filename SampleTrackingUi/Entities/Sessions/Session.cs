using System;

namespace SampleTrackingUi.Models.Sessions
{
    public class Session
    {
        public int SessionId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public char StatusId { get; set; }
        public int SessionTypeId { get; set; }
        public int TrayId { get; set; }
        public string TrayCode { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime CloseDate { get; set; }
        public int ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}
