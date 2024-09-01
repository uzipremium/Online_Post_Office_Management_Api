using MediatR;
using Online_Post_Office_Management_Api.Models;
using System.Collections.Generic;

namespace Online_Post_Office_Management_Api.Queries.Deliveries
{
    public class DeliveryGetAll : IRequest<List<Delivery>>
    {
    }
}
