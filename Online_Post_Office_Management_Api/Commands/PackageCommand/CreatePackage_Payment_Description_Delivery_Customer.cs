using MediatR;
using Online_Post_Office_Management_Api.Models;

namespace Online_Post_Office_Management_Api.Commands.PackageCommand
{
    public class CreatePackage_Payment_Description_Delivery_Customer : IRequest<Models.Package>
    {
        public Models.Package Package { get; set; }
        public Models.Payment Payment { get; set; }
        public Models.Description Description { get; set; }
        public Models.Delivery Delivery { get; set; }
        public Models.Customer Customer { get; set; }

        public CreatePackage_Payment_Description_Delivery_Customer(Models.Package package, Models.Payment payment, Models.Description description, Models.Delivery delivery, Models.Customer customer)
        {
            Package = package;
            Payment = payment;
            Description = description;
            Delivery = delivery;
            Customer = customer;
        }
    }
}
