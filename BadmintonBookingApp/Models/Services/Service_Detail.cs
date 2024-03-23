namespace BadmintonBookingApp.Models.Services
{
    public class Service_Detail
    {
        public int Id { get; set; }
        //public int ServiceReceiptId { get; set; }   
        //public int ServiceId { get; set; }  

        public int Quantity { get; set; }

        public Service_Receipt Service_Receipt { get; set; }
        public Service Service { get; set; }
    }
}
