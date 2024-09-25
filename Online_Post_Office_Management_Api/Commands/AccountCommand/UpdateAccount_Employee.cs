using MediatR;
using Online_Post_Office_Management_Api.DTO;
using System;

namespace Online_Post_Office_Management_Api.Commands.AccountCommand
{
    public class UpdateAccount_Employee : IRequest<EmployeeWithAccountWithOfficeDto>
    {
        public string AccountId { get; set; }
        public string EmployeeId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string OfficeId { get; set; }
        public string Password { get; set; }

        public UpdateAccount_Employee() { }

        public UpdateAccount_Employee(
            string accountId, string employeeId, string email, string phone, string gender,
            string name, DateTime dateOfBirth, string officeId, string password)
        {
            AccountId = accountId;
            EmployeeId = employeeId;
            Email = email;
            Phone = phone;
            Gender = gender;
            Name = name;
            DateOfBirth = dateOfBirth;
            OfficeId = officeId;
            Password = password;
        }
    }
}
