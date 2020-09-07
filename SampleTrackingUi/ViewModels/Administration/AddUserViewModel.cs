using SampleTrackingUi.ApiModels.Administration;
using SampleTrackingUi.Models.Administration;
using System.Collections.Generic;

namespace SampleTrackingUi.ViewModels.Administration
{
    public class AddUserViewModel
    {
        public AddUserViewModel()
        {
            User = new UserAddUpdateModel();
            User.Status = 'A';
            ErrorList = new List<string>();
        }

        public string PageTitle { get; set; }

        public UserAddUpdateModel User { get; set; }

        public List<ApplicationRoleModel> ApplicationRoles { get; set; }
        public List<string> ErrorList { get; set; }
    }
}
