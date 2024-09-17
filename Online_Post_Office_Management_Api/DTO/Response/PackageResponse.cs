namespace Online_Post_Office_Management_Api.DTO.Response
{
    public class PackageResponse
    {
        public string Id { get; set; }
        public string SenderName { get; set; }
        public string OfficeId { get; set; }
        public string OfficeName { get; set; }
        public string ServiceName { get; set; }
        public decimal Weight { get; set; }
        public decimal Distance { get; set; }
        public string DeliveryNumber { get; set; }
        public string Description { get; set; }
        public string PaymentStatus { get; set; }
        public string DeliveryStatus { get; set; }
        public string Receiver { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
