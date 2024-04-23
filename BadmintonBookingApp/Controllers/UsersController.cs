using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BadmintonBookingApp.Data;
using BadmintonBookingApp.Models.User;
using BadmintonBookingApp.ViewModels;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace BadmintonBookingApp.Controllers
{
    [Area("Admin"), Authorize(Roles = "Admin")]

    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public UsersController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var usersViewModel = new UserViewModel
            {
                Users = await _context.Users.ToListAsync()
            };

            return View(usersViewModel);
        }


        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Email,EmailConfirmed, FullName,Status,ImageUrl")] AppUser user)
        {
       
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid && !user.Email.IsNullOrEmpty())
            {
                try
                {
                    var currentUser = await _context.Users.FindAsync(user.Id);
                    if (currentUser == null)
                    {
                        return NotFound();
                    }

                    // Update the properties of the current user with the values from the edited user
                    currentUser.FullName = user.FullName;
                    currentUser.Status = user.Status;
                    currentUser.ImageUrl = user.ImageUrl;
                    currentUser.PhoneNumber = user.PhoneNumber;
                    currentUser.UserName = user.UserName;
                    currentUser.Email = user.Email;
                    currentUser.EmailConfirmed = user.EmailConfirmed;
                    _context.Entry(currentUser).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }


        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
        [HttpPost]
  
        public async Task<IActionResult> ChangeStatus(string id)
        {
                try
                {
                    var currentUser = await _context.Users.FindAsync(id);
                    if (currentUser == null)
                    {
                        return NotFound();
                    }

                    // Update the properties of the current user with the values from the edited user
            
                    currentUser.Status =  currentUser.Status == 1 ? 0 : 1;
   
                    _context.Entry(currentUser).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(id))
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

    }
}
