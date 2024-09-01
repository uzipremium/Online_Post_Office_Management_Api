using MediatR;
using Online_Post_Office_Management_Api.Models;

namespace Online_Post_Office_Management_Api.Commands
{
    public class CreatePackage_Payment_Description_Delivery : IRequest<Package>
    {
        public Package Package { get; set; }
        public Payment Payment { get; set; }
        public Description Description { get; set; }
        public Delivery Delivery { get; set; }

        public CreatePackage_Payment_Description_Delivery(Package package, Payment payment, Description description, Delivery delivery)
        {
            Package = package;
            Payment = payment;
            Description = description;
            Delivery = delivery;
        }
    }
}
