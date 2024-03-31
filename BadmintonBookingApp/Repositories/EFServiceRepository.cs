using BadmintonBookingApp.Data;
using BadmintonBookingApp.Models.Services;
using Microsoft.EntityFrameworkCore;
namespace BadmintonBookingApp.Repositories
{
    public class EFServiceRepository : IServiceRepository
    {
        private readonly ApplicationDbContext _context;

        public EFServiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Service>> GetAllAsync()
        {
            return await _context.Services.ToListAsync();
        }
        public async Task<Service> GetByIdAsync(int id)
        {
            return await _context.Services.FindAsync(id);
        }
        public async Task AddAsync(Service service)
        {
            _context.Services.Add(service);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Service service)
        {
            _context.Services.Update(service);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var category = await _context.Services.FindAsync(id);
            _context.Services.Remove(category);
            await _context.SaveChangesAsync();
        }

        public bool IsNone(int id)
        {
            var product = _context.Services.FirstOrDefault(p => p.Id == id);
            return (product == null) ? true : false;
        }
    }
}
