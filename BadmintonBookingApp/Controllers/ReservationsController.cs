using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BadmintonBookingApp.Data;
using BadmintonBookingApp.Models.Reservations;
using Microsoft.AspNetCore.Identity;
using BadmintonBookingApp.Models.User;
using BadmintonBookingApp.Models.Managements;
using System.Security.Claims;
using NuGet.Protocol.Plugins;
using Azure.Messaging;
using BadmintonBookingApp.Repositories;
using BadmintonBookingApp.Models.Padding;
using BadmintonBookingApp.Models.Services;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace BadmintonBookingApp.Controllers
{
    public class ReservationsController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IReservation _eFReservation;
        //public static int CurrentRev;
        public static Reservation tempRev;
        public static DateTime b;
        public static DateTime s;
        public static DateTime e;

        public ReservationsController(ApplicationDbContext context, UserManager<AppUser> userManager, IReservation eFReservation)
        {
            _context = context;
            _userManager = userManager;
            _eFReservation = eFReservation;
        }
        //[Authorize(Roles = "Admin")]
        // GET: Reservations
        public async Task<IActionResult> Index(int pageNumber = 1)
        {

            if (User.Identity.IsAuthenticated)
            {
                IQueryable<Reservation> reservationQuery;
                if (User.IsInRole("Admin"))
                    reservationQuery = _context.Reservations.Include(p => p.Price);
                else
                {
                    var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    reservationQuery = _context.Reservations.Where(x => x.UserId == id).Include(p => p.Price);
                }
                int pageSize = 20;
                //return View(await _context.Reservations.ToListAsync());

                var paginatedReservations = await PaginatedList<Reservation>.CreateAsync(reservationQuery, pageNumber, pageSize);
                return View(paginatedReservations);
            }
            return RedirectToAction(nameof(HomeController.Index),"Home");
        }

        // GET: Reservations/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        public async Task<IActionResult> SearchReservations(DateTime startDate, DateTime endDate, int pageNumber = 1)
        {
            if (User.Identity.IsAuthenticated)
            {
                IQueryable<Reservation> reservationQuery;
                if (User.IsInRole("Admin"))
                {
                    if (startDate == default(DateTime) && endDate == default(DateTime))
                    {
                        reservationQuery = _context.Reservations.Include(p => p.Price);
                    }
                    else
                    {
                        reservationQuery = _context.Reservations.Where(p => startDate <= p.BookingDate && p.BookingDate <= endDate).Include(p => p.Price); ;
                    }
                }
                else
                {
                    var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    if (startDate == default(DateTime) && endDate == default(DateTime))
                    {
                        reservationQuery = _context.Reservations.Where(x => x.UserId == id).Include(p => p.Price);
                    }
                    else
                    {
                        reservationQuery = _context.Reservations.Where(x => x.UserId == id).Where(p => startDate <= p.BookingDate && p.BookingDate <= endDate).Include(p => p.Price); ;
                    }
                }
                var paginatedReservations = await PaginatedList<Reservation>.CreateAsync(reservationQuery, pageNumber, 20);
                return PartialView("_ReservationsSearchResult", paginatedReservations);

            }
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        // GET: Reservations/Create

        public IActionResult Create()
        {
            Reservation tempRev = null;
            RF_DetailController.listRFD.Clear();
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Customer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookingDate,StartTime,EndTime")] Reservation reservation)
        {

            if (ModelState.IsValid)
            {
                b = reservation.BookingDate;
                s = reservation.StartTime;
                e = reservation.EndTime;
                reservation.StartTime = new DateTime(b.Year, b.Month, b.Day, s.Hour, s.Minute, s.Second);
                reservation.EndTime = new DateTime(b.Year, b.Month, b.Day, e.Hour, e.Minute, e.Second);
                s = reservation.StartTime;
                e = reservation.EndTime;
                if (_eFReservation.TimeIsValid(reservation.BookingDate, reservation.StartTime, reservation.EndTime) != 0)
                {
                    int i = _eFReservation.TimeIsValid(reservation.BookingDate, reservation.StartTime, reservation.EndTime);
                    if (i == 1)
                        ViewBag.Message = string.Format("Booking date cannot be earlier than current date");
                    else if (i == 2)
                        ViewBag.Message = string.Format("Minimun duration is 30 minutes");
                    else if (i == 3)
                        ViewBag.Message = string.Format("Strating time cannot be earlier than current time");
                    else
                        ViewBag.Message = string.Format("There is no available court for this period of time");
                    return View(reservation);
                }
                reservation.PriceId = _context.Prices.FirstOrDefault().Id;
                //reservation.Price = _context.Prices.FirstOrDefault();
                reservation.Deposite = 50000;
                reservation.CreateDate = DateTime.Now;
                reservation.Status = 0;
                reservation.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                tempRev = reservation;
                //_context.Reservations.Add(reservation);
                //await _context.SaveChangesAsync();
                //CurrentRev= reservation.Id;

                return RedirectToAction("RFD", "RF_Detail");
            }
            return View(reservation);
        }


        // GET: Reservations/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Deposite,CreateDate,BookingDate,StartTime,EndTime,Status")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Cancel()
        {
            //var reservation = await _context.Reservations.FindAsync(CurrentRev);
            //if(reservation != null)
            //    _context.Reservations.Remove(reservation);
            //CurrentRev = -1;
            return RedirectToAction("Create");
        }
        public async Task<IActionResult> DetailRFD(int id)
        {
            return View(_context.RF_Details.Include(p => p.Court).Where(f => f.ReservationId == id).ToList());
        }
        public async Task<IActionResult> CancelDetail(int id)
        {
            Reservation rev = _context.Reservations.Include(p=>p.RF_Details).FirstOrDefault(p=>p.Id==id);

            if(rev != null)
            {
                int d = rev.Id;
                List<RF_Detail> listRFD = _context.RF_Details.Where(p => p.ReservationId == d).ToList();
                foreach(RF_Detail detail in listRFD)
                {
                    _context.RF_Details.Remove(detail);
                    await _context.SaveChangesAsync();
                }
                rev.Status = 7;
                _context.Reservations.Update(rev);
                await _context.SaveChangesAsync();
            }
             
            return RedirectToAction("Index");
        }
    }
}
