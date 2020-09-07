using System;
using System.Runtime.Serialization;

namespace SampleTrackingUi.ApiModels.Administration
{
    public class UserApi
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameNormalized { get; set; }
        public string Email { get; set; }
        public string EmailNormalized { get; set; }
        public char Status { get; set; }
        public string PasswordHash { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }

    }
}
