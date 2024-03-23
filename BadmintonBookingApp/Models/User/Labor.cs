using BadmintonBookingApp.Models.Receipts;
using BadmintonBookingApp.Models.Reservations;
using BadmintonBookingApp.Models.Services;

namespace BadmintonBookingApp.Models.User
{
    public class Labor : User
    {
        public List<Receipt>? Receipts { get; set; }
    }
}
