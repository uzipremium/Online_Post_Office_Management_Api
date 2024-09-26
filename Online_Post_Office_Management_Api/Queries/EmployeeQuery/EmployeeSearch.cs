namespace Online_Post_Office_Management_Api.Queries.EmployeeQuery
{
    public class EmployeeSearch
    {
        public string Name { get; set; }
        public string OfficeId { get; set; }
        public string Phone { get; set; }
        public string OfficeName { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public EmployeeSearch(string name = null, string officeId = null, string phone = null, string officeName = null, int pageNumber = 1, int pageSize = 10)
        {
            Name = name;
            OfficeId = officeId;
            Phone = phone;
            OfficeName = officeName;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
        public EmployeeSearch()
        {
            PageNumber = 1;  
            PageSize = 10;   
        }
    }
}
