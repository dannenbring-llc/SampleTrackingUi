using System;

namespace SampleTrackingUi.ViewModels
{
    public class ApplicationUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserNameNormalized { get; set; }
        public string UserLongName { get; set; }
        public string Email { get; set; }
        public string EmailNormalized { get; set; }
        public string PasswordHash { get; set; }
        public char Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int ModifyBy { get; set; }
        public DateTime ModifyDateTime { get; set; }
    }
}
