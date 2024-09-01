using MediatR;
using Online_Post_Office_Management_Api.Models;

namespace Online_Post_Office_Management_Api.Queries.Deliveries
{
    public class DeliveryGetOne : IRequest<Delivery>
    {
        public string Id { get; set; }

        public DeliveryGetOne(string id)
        {
            Id = id;
        }
    }
}
