﻿using BadmintonBookingApp.Models.Receipts;
using BadmintonBookingApp.Models.Reservations;
using BadmintonBookingApp.Models.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BadmintonBookingApp.Models.User
{
    public class AppUser : IdentityUser
    {
 
        public string FullName { get; set; }

        public int Status { get; set; }

        public string? ImageUrl { get; set; }    
        public List<Service_Receipt>? Service_Receipts { get; set; }
        public List<Reservation>? Reservations { get; set; }
        [ValidateNever]
        public List<Receipt>? Receipts { get; set; } 
    }
}
