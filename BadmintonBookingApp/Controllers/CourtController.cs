using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BadmintonBookingApp.Data;
using BadmintonBookingApp.Models.Facilities;
using BadmintonBookingApp.Repositories;

namespace BadmintonBookingApp.Controllers
{
    public class CourtController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICourtRepository _courtRepository;
        private static DateTime _dateTime;

        public CourtController(ApplicationDbContext context,ICourtRepository courtRepository)
        {
            _context = context;
            _courtRepository = courtRepository;
        }

        // GET: Court
        public async Task<IActionResult> Index()
        {
            //return View(await _context.Courts.ToListAsync());
            return View(await _courtRepository.GetAllAsync());
        }

        // GET: Court/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var court = await _context.Courts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (court == null)
            {
                return NotFound();
            }

            return View(court);
        }

        // GET: Court/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Court/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CourtName,StateDate,Status")] Court court)
        {
            court.StateDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(court);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(court);
        }

        // GET: Court/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var court = await _context.Courts.FindAsync(id);
            if (court == null)
            {
                return NotFound();
            }
            _dateTime = court.StateDate;
            return View(court);
        }
        // POST: Court/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourtName,StateDate,Status")] Court court)
        {
            if (id != court.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    court.StateDate = _dateTime;
                    _context.Update(court);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourtExists(court.Id))
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
            return View(court);
        }

        // GET: Court/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var court = await _courtRepository.GetByIdAsync(id);
            if (court == null)
            {
                return NotFound();
            }

            return View(court);
        }

        // POST: Court/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var court = await _context.Courts.FindAsync(id);
            //if (court != null)
            //{
            //    _context.Courts.Remove(court);
            //}

            await _courtRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool CourtExists(int id)
        {
            return _context.Courts.Any(e => e.Id == id);
        }
    }
}
