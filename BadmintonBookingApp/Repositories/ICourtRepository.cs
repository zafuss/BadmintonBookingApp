using BadmintonBookingApp.Models.Facilities;

namespace BadmintonBookingApp.Repositories
{
    public interface ICourtRepository
    {
        Task<IEnumerable<Court>> GetAllAsync();
        Task<Court> GetByIdAsync(int id);
        Task AddAsync(Court court);
        Task UpdateAsync(Court court);
        Task DeleteAsync(int id);
    }
}
