using BadmintonBookingApp.Models.Services;
namespace BadmintonBookingApp.Repositories
{
    public interface IServiceRepository
    {
        Task<IEnumerable<Service>> GetAllAsync();

        Task<IQueryable<Service>> GetServices();
        Task<Service> GetByIdAsync(int id);
        Task AddAsync(Service service);
        Task UpdateAsync(Service service);
        Task DeleteAsync(int id);
    }
}
