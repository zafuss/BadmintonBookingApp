using BadmintonBookingApp.Models.User;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BadmintonBookingApp.Models.Services
{
    public class Service_Receipt
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        [Precision(18, 2)]
        public decimal Total { get; set; }

        //public int UserId { get; set; }

        public Customer? Customer { get; set; }

        public Labor Labor { get; set; }

        public List<Service_Detail>? Service_Details { get; set; }
    }
}
