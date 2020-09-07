using System.Runtime.Serialization;

namespace SampleTrackingUi.ApiModels.Administration
{
    public class ApplicationRoleApi
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string NormalizedName { get; set; }
    }
}
