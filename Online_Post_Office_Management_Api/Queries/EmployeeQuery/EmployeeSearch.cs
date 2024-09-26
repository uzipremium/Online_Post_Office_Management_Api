namespace Online_Post_Office_Management_Api.Queries.EmployeeQuery
{
    public class EmployeeSearch
    {
        public string Name { get; set; }
        public string OfficeId { get; set; }
        public string Phone { get; set; }
        public string OfficeName { get; set; }

      
        public EmployeeSearch(string name = null, string officeId = null, string phone = null, string officeName = null)
        {
            Name = name;
            OfficeId = officeId;
            Phone = phone;
            OfficeName = officeName; 
        }

        
        public EmployeeSearch() { }
    }
}
