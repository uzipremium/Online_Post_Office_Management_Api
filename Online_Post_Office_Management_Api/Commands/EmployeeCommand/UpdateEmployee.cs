using MediatR;
using Online_Post_Office_Management_Api.DTO;

namespace Online_Post_Office_Management_Api.Commands.EmployeeCommand
{
    public class UpdateEmployee : IRequest<EmployeeWithOfficeDto> 
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string OfficeId { get; set; }
        public string OfficeName{ get; set; }
        public string AccountId { get; set; }
        public DateTime CreatedDate { get; set; }

        public UpdateEmployee(string id, string email, string phone, string gender, string name, DateTime dateOfBirth, string officeId, string accountId, DateTime createdDate)
        {
            Id = id;
            Email = email;
            Phone = phone;
            Gender = gender;
            Name = name;
            DateOfBirth = dateOfBirth;
            OfficeId = officeId;
            AccountId = accountId;
            CreatedDate = createdDate;
        }

    }
}
