using BadmintonBookingApp.Data;
using BadmintonBookingApp.Models.Facilities;
using BadmintonBookingApp.Models.Reservations;
using BadmintonBookingApp.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BadmintonBookingApp.Repositories
{
    public interface IReservation
    {
        Task<IEnumerable<Reservation>> GetAllAsync();
        Task<Reservation> GetByIdAsync(int? id);
        Task AddAsync(Reservation reservation);
        Task UpdateAsync(Reservation reservation);
        Task DeleteAsync(int? id);
        int TimeIsValid(DateTime b, DateTime s, DateTime e);
        bool ValidCourt(DateTime b, DateTime s, DateTime e, Court c, List<RF_Detail> listRF_DETAIL);
        List<Court> GetAllValidCourt(DateTime b, DateTime s, DateTime e);
    }
   
}
