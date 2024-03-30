using BadmintonBookingApp.Models.Services;
using BadmintonBookingApp.Models.Facilities;
using BadmintonBookingApp.Repositories;

namespace BadmintonBookingApp.Models
{
    public class UserModel
    {
        public IEnumerable<Service> services { get; set; }
        public IEnumerable<Court> courts { get; set; }

        public UserModel(IEnumerable<Service> _services, IEnumerable<Court> _courts) 
        { 
            services = _services;
            courts = _courts;
        }
    }
}
