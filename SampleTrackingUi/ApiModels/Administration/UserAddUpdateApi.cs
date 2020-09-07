using System.Runtime.Serialization;

namespace SampleTrackingUi.ApiModels.Administration
{
    public class UserAddUpdateApi
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public char Status { get; set; }
        public string PasswordHash { get; set; }
        public int RoleId { get; set; }
        public int UpdatedByUser { get; set; }
    }
}
