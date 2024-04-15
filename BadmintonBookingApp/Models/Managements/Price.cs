using BadmintonBookingApp.Models.Reservations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BadmintonBookingApp.Models.Managements
{
    public class Price
    {
        public int Id { get; set; }
        [Precision(18, 2)]
        public decimal PriceTag { get; set; }
        public float TimeFactor { get; set; }
        public float DateFactor { get; set; }
        public int Status { get; set; }

        public List<Reservation>? Reservations { get; set; }
    }
}
