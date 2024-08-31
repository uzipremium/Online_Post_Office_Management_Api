using MediatR;

namespace Online_Post_Office_Management_Api.Commands
{
    public class UpdateEmployee : IRequest<int>
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string OfficeId { get; set; }

        public UpdateEmployee(string id, string email, string phone, string gender, string name, DateTime dateOfBirth, string officeId)
        {
            Id = id;
            Email = email;
            Phone = phone;
            Gender = gender;
            Name = name;
            DateOfBirth = dateOfBirth;
            OfficeId = officeId;
        }
    }
}
