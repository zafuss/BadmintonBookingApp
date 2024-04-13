using BadmintonBookingApp.Models.Facilities;
using BadmintonBookingApp.Models.Managements;
using BadmintonBookingApp.Models.Receipts;
using BadmintonBookingApp.Models.Reservations;
using BadmintonBookingApp.Models.Services;
using BadmintonBookingApp.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BadmintonBookingApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<IdentityUser> IdentityUsers { get; set; }   
        public DbSet<AppUser> Users {  get; set; } 
        public DbSet<Branch> Branches { get; set; } 
        public DbSet<Court> Courts { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<RF_Detail> RF_Details { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Service_Detail> Service_Details { get; set; }
        public DbSet<Service_Receipt> Service_Receipts { get; set; }




    }
}
