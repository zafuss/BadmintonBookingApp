using BadmintonBookingApp.Models.Services;
using BadmintonBookingApp.Models.Facilities;
using BadmintonBookingApp.Repositories;
using BadmintonBookingApp.Models.Padding;
namespace BadmintonBookingApp.Models
{
    public class UserModel
    {
        public PaginatedList<Service> services { get; set; }
        public IEnumerable<Court> courts { get; set; }

        public UserModel(PaginatedList<Service> _services, IEnumerable<Court> _courts) 
        { 
            services = _services;
            courts = _courts;
        }
    }
}
