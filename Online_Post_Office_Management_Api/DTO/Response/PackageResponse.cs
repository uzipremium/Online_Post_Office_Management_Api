namespace Online_Post_Office_Management_Api.DTO.Response
{
    public class PackageResponse
    {
        public string Id { get; set; }
        public string SenderName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }

        public string OfficeId { get; set; }
        public string OfficeName { get; set; }
        public string OfficeAddress { get; set; }
        public string OfficeState { get; set; }
        public string OfficeCity { get; set; }
        public string OfficePinCode { get; set; }
        public string OfficePhone { get; set; }

        public string ServiceName { get; set; }
        public decimal Weight { get; set; }
        public decimal Distance { get; set; }

        public string DeliveryId { get; set; }
        public string DeliveryNumber { get; set; }

        public string DescriptionText { get; set; }

        public string ReceiverAddress { get; set; }

        public string PaymentId { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime TransactionTime { get; set; }
        public decimal PaymentCost { get; set; }

        public DateTime SendDate { get; set; }
        public string DeliveryStatus { get; set; }
        public string EndOfficeName { get; set; }
        public DateTime? DeliveryDate { get; set; }


        public string Receiver { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
