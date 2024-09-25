namespace Online_Post_Office_Management_Api.DTO
{
    public class EmployeeWithAccountWithOfficeDto
    {
        public string EmployeeId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedDate { get; set; }
        public string OfficeId { get; set; }
        public string OfficeName { get; set; } 
        public string AccountId { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }
        public string RoleId { get; set; }
    }
}
