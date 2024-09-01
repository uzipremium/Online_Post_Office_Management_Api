namespace Online_Post_Office_Management_Api.Repositories
{
    public interface IPaymentRepository
    {
        Task<bool> UpdatePaymentStatusAsync(string paymentId, string status, DateTime transactionTime);
    }
}
