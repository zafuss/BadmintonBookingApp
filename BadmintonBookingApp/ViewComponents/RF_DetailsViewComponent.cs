using BadmintonBookingApp.Controllers;
using BadmintonBookingApp.Data;
using BadmintonBookingApp.Models.Reservations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace BadmintonBookingApp.ViewComponents
{
    [ViewComponent(Name ="RF_Details")]
    public class RF_DetailsViewComponent : ViewComponent
    {
        
        private readonly ApplicationDbContext _context;
        public RF_DetailsViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            int a = ReservationsController.CurrentRev;
            var item = await GetRecenrRFD(a);
            return View(item);
        }

        public async Task<List<RF_Detail>> GetRecenrRFD(int rev)
        {
            return await _context.RF_Details.Include(p=>p.Reservation).Include(p=>p.Court).Where(p=>p.ReservationId==rev).ToListAsync();
        }
        
    }
}
