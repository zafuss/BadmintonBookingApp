using BadmintonBookingApp.Controllers;
using BadmintonBookingApp.Data;
using BadmintonBookingApp.Models.Facilities;
using BadmintonBookingApp.Models.Reservations;
using Microsoft.EntityFrameworkCore;

namespace BadmintonBookingApp.Repositories
{
    public class EFReservation : IReservation
    {
        private readonly ApplicationDbContext _context;
        public EFReservation(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Reservation>> GetAllAsync()
        {
            return await _context.Reservations.Include(p=>p.AppUser).ToListAsync();
        }
        public async Task<Reservation> GetByIdAsync(int? id)
        {
            return await _context.Reservations.FindAsync(id);
        }
        public async Task AddAsync(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int? id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                reservation.Status = -1;
            }
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();
        }
        public int TimeIsValid(DateTime b, DateTime s, DateTime e)
        {
            if (b.Date < DateTime.Now.Date)
                return 1;
            if (DateTime.Compare(s, e) > 0 || (e.Hour * 60 - s.Hour * 60 + e.Minute - s.Minute) < 30)
                return 2;
            if (DateTime.Compare(s, DateTime.Now) <= 0)
                return 3;
            if (GetAllValidCourt(b, s, e).Count == 0)
                return 4;
            return 0;
        }
        public bool ValidCourt(DateTime b, DateTime s, DateTime e,Court c,List<RF_Detail> listRF_DETAIL)
        {
            var listRF = listRF_DETAIL.Where(p => p.CourtId == c.Id).ToList();
            if (listRF.Count == 0)
                return true;
            foreach (var item in listRF)
            {
                int d1 = DateTime.Compare(s, item.Reservation.EndTime);
                int d2 = DateTime.Compare(e, item.Reservation.StartTime);
                if (d1 > 0 || d2 < 0)
                    continue;
                else
                    return false;
            }
            return true;
        }
        public List<Court> GetAllValidCourt(DateTime b, DateTime s, DateTime e)
        {
            var listC = _context.Courts.ToList();
            if(RF_DetailController.listRFD!=null)
            {
                foreach (var item in RF_DetailController.listRFD)
                {
                    listC.Remove(_context.Courts.FirstOrDefault(p => p.Id == item.CourtId));
                }
            }
            var listRF_DETAIL = _context.RF_Details.Include(p => p.Reservation).Where(p =>p.Reservation.BookingDate.Date == b.Date).ToList();
            if(listRF_DETAIL.Count == 0) return listC;
            foreach (var item in _context.Courts.ToList())
            {
                if(!ValidCourt(b,s,e,item,listRF_DETAIL))
                    listC.Remove(item);
            }
            return listC;
        }
    }
}
