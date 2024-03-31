using BadmintonBookingApp.Data;
using BadmintonBookingApp.Models.Facilities;
using Microsoft.EntityFrameworkCore;
namespace BadmintonBookingApp.Repositories
{
    public class EFCourtRepository : ICourtRepository
    {
        private readonly ApplicationDbContext _context;

        public EFCourtRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Court>> GetAllAsync()
        {
            return await _context.Courts.ToListAsync();
        }
        public async Task<Court> GetByIdAsync(int id)
        {
            return await _context.Courts.FindAsync(id);
        }
        public async Task AddAsync(Court court)
        {
            _context.Courts.Add(court);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Court court)
        {
            _context.Courts.Update(court);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var court = await _context.Courts.FindAsync(id);
            _context.Courts.Remove(court);
            await _context.SaveChangesAsync();
        }

    }
}
