using System.Collections.Generic;
using BadmintonBookingApp.Models.User;
using Microsoft.AspNetCore.Identity;

namespace BadmintonBookingApp.ViewModels
{
    public class UserViewModel
    {
        public IEnumerable<AppUser> Users { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}
