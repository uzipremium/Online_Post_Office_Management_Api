using MediatR;
using Online_Post_Office_Management_Api.Models;
using System;
using System.Collections.Generic;

namespace Online_Post_Office_Management_Api.Queries.PaymentQuery
{
    public class PaymentGetAll : IRequest<List<Payment>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        // Đặt PaymentStatus là tùy chọn (nullable)
        public string? PaymentStatus { get; set; }

        // Đặt StartDate là nullable
        public DateTime? StartDate { get; set; }

        // Constructor không tham số để hỗ trợ Model Binding
        public PaymentGetAll() { }

        // Constructor với tham số
        public PaymentGetAll(int pageNumber, int pageSize, string? paymentStatus, DateTime? startDate)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            PaymentStatus = paymentStatus;
            StartDate = startDate;
        }
    }
}
