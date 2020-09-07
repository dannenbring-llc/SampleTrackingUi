namespace SampleTrackingUi.ApiModels.Account
{
    public class Employee
    {
        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name
        {
            get
            {
                if (LastName != null && FirstName != null)
                    return $"{LastName}, {FirstName}";
                else
                    return null;
            }
        }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
