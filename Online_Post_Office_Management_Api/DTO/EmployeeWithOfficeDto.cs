namespace Online_Post_Office_Management_Api.DTO
{
    public class EmployeeWithOfficeDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string OfficeId { get; set; }
        public string OfficeName { get; set; }
        public string AccountId { get; set; }
    }
}
