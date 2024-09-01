using Online_Post_Office_Management_Api.Models;

namespace Online_Post_Office_Management_Api.DTO
{
    public class Package_Payment_Description_DeliveryDto
    {
        public Package package{get;set;}
        public Payment payment{get;set;}
        public Description description{get;set;}
        public Delivery delivery{get;set;}
    }
}
