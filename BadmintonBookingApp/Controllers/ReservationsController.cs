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

namespace BadmintonBookingApp.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IReservation _eFReservation;
        public static int CurrentRev;
        static DateTime b;
        static DateTime s;
        static DateTime e;

        public ReservationsController(ApplicationDbContext context, UserManager<AppUser> userManager, IReservation eFReservation)
        {
            _context = context;
            _userManager = userManager;
            _eFReservation = eFReservation;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reservations.ToListAsync());
        }

        // GET: Reservations/Details/5
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
        
        
        // GET: Reservations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookingDate,StartTime,EndTime")] Reservation reservation)
        {
            if(_context.Prices.FirstOrDefault() == null)
            {
                Price pr = new Price();
                pr.Id = "abcdef";
                pr.Status = 1;
                pr.DateFactor = 1;
                pr.TimeFactor = 1;
                pr.PriceTag = 50000;
            }
            if (ModelState.IsValid)
            {
                b = reservation.BookingDate;
                s = reservation.StartTime;
                e = reservation.EndTime;
                reservation.StartTime = new DateTime(b.Year, b.Month, b.Day, s.Hour, s.Minute, s.Second);
                reservation.EndTime = new DateTime(b.Year, b.Month, b.Day, e.Hour, e.Minute, e.Second);
                s = reservation.StartTime;
                e = reservation.EndTime;
                if (!_eFReservation.TimeIsValid(reservation.BookingDate,reservation.StartTime,reservation.EndTime))
                {
                    return View(reservation);
                }
                reservation.Price = _context.Prices.FirstOrDefault();
                reservation.Deposite = 50000;
                reservation.CreateDate = DateTime.Now;
                reservation.Status = 0;
                reservation.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Reservations.Add(reservation);
                await _context.SaveChangesAsync();
                CurrentRev= reservation.Id;
                return RedirectToAction("RFD");
            }
            return View(reservation);
        }
        
        public IActionResult RFD()
        {
            ViewBag.Rev = CurrentRev;
            ViewBag.Court = new SelectList(_eFReservation.GetAllValidCourt(b,s,e), "Id","CourtName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RFD([Bind("Note,ReservationId,CourtId")] RF_Detail rf)
        {
            
            if(ModelState.IsValid)
            {
                
                rf.ReservationId = CurrentRev;
                /* var listRFD = new List<RF_Detail>();
                listRFD.Add(rf);
                if (listRFD.Count > 3) ;*/
                _context.RF_Details.Add(rf);
                await _context.SaveChangesAsync();
                ViewBag.Rev = CurrentRev;
                ViewBag.Court = new SelectList(_eFReservation.GetAllValidCourt(b, s, e), "Id", "CourtName");
                return View(rf);
            }
            ViewBag.Rev = CurrentRev;
            ViewBag.Court = new SelectList(_eFReservation.GetAllValidCourt(b, s, e), "Id", "CourtName");
            return RedirectToAction("RFD");
        }
        public IActionResult Finish()
        {
            var rf = _context.RF_Details.Where(p=>p.ReservationId == CurrentRev).Include(p=>p.Court).ToList();
            return View(rf);
        }
        // GET: Reservations/Edit/5
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
    }
}
