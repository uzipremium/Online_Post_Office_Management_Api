namespace Online_Post_Office_Management_Api.Repositories
{
    public interface IPricingServiceRepository
    {
        Task<decimal> CalculateDeliveryPriceAsync(string serviceType, double weight, double distance);
    }
}
