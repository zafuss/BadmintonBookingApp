using BadmintonBookingApp.Models.Managements;
using BadmintonBookingApp.Models.Receipts;
using BadmintonBookingApp.Models.User;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BadmintonBookingApp.Models.Reservations
{
    public class Reservation
    {
        public int Id { get; set; }

        //public int UserId { get; set; } 
        [Precision(18, 2)]
        public decimal Deposite { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Status { get; set; }
        public string? UserId { get; set; }
        public int PriceId { get; set; }
        [ValidateNever]
        public Price? Price { get; set; }
        public AppUser? AppUser { get; set; }
        public Receipt? Receipt { get; set; }
        public List<RF_Detail>? RF_Details { get; set; }
    }
}
