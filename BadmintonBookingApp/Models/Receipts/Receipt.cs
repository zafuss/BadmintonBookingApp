using BadmintonBookingApp.Models.Reservations;
using BadmintonBookingApp.Models.User;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BadmintonBookingApp.Models.Receipts
{
    public class Receipt
    {
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }
        [Precision(18, 2)]
        public decimal Total { get; set; }
        public float ExtraTime { get; set; }
        public int ReservationId { get; set; }
        //public int UserId { get; set; }

        public string Payment { get; set; }

        public Reservation? Reservation { get; set; }

        public AppUser Labor { get; set; }
    }
}
