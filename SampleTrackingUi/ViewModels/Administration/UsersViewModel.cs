using SampleTrackingUi.Models.Administration;
using System.Collections.Generic;

namespace SampleTrackingUi.ViewModels.Administration
{
    public class UsersViewModel
    {
        public UsersViewModel()
        { }

        public string PageTitle { get; set; }

        public List<UserModel> Users { get; set; }
        public List<ApplicationRoleModel> ApplicationRole { get; set; }
    }
}
