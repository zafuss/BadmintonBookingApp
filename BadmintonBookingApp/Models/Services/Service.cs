using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BadmintonBookingApp.Models.Services
{
    public class Service
    {

        public int Id { get; set; }

        public string ServiceName { get; set; }

        public string Unit { get; set; }

        [Precision(18, 2)]
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string Status { get; set; }

        public string? ImageUrl { get; set; }    
        public List<Service_Detail>? Service_Details { get; set; }
    }
}
