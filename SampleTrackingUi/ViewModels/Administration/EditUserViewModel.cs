using SampleTrackingUi.Models.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleTrackingUi.ViewModels.Administration
{
    public class EditUserViewModel
    {
        public string PageTitle { get; set; }

        public UserModel User { get; set; }

        public List<ApplicationRoleModel> ApplicationRoles { get; set; }
    }
}
