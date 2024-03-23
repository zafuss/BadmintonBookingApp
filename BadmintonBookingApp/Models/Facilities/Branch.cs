namespace BadmintonBookingApp.Models.Facilities
{
    public class Branch
    {
        public string Id { get; set; }

        public string BranchName { get; set; }

        public string Address { get; set; }

        public List<Court>? Courts { get; set; }
    }
}
