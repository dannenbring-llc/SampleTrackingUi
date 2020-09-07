using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleTrackingUi.Models.Administration
{
    public class UserAddUpdateModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public char Status { get; set; }
        public string PasswordHash { get; set; }
        public int RoleId { get; set; }
        public int UpdatedByUser { get; set; }
    }
}
