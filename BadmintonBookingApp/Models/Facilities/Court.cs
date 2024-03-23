using BadmintonBookingApp.Models.Reservations;

namespace BadmintonBookingApp.Models.Facilities
{
    public class Court
    {
        public int Id { get; set; }
        public string CourtName { get; set; }
        public DateTime StateDate { get; set; }
        //public string BranchId { get; set; }
        public int Status { get; set; }
        public Branch? Branch { get; set; }

        public List<RF_Detail>? RF_Details { get; set; }
    }
}
