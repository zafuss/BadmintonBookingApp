﻿using BadmintonBookingApp.Models.Facilities;

namespace BadmintonBookingApp.Models.Reservations
{
    public class RF_Detail
    {
        public int Id { get; set; }
        //public string ReservationId { get; set; }   
        //public string CourtId { get; set;}

        public string Note { get; set; }

        public Reservation? Reservation { get; set; }

        public Court? Court { get; set; }
    }
}
