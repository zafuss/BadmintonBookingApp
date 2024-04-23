using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BadmintonBookingApp.Data;
using BadmintonBookingApp.Models.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using BadmintonBookingApp.Models.Facilities;
using Microsoft.IdentityModel.Tokens;
using BadmintonBookingApp.Models.Padding;
using static System.Net.Mime.MediaTypeNames;

namespace BadmintonBookingApp.Areas.Admin.Controllers
{
    [Area("Admin"),Authorize(Roles ="Admin")]

    public class ServicesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private static string? _image;
        public ServicesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/Services
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            int pageSize = 10;
            //         if (Test == null)
            //{
            //             IQueryable<Product> productsQuery = _context.Products.Include(p => p.Category);
            //             var paginatedProducts = await PaginatedList<Product>.CreateAsync(productsQuery, pageNumber, pageSize);
            //             return View(paginatedProducts);
            //         }
            //         else
            //         {
            //             IQueryable<Product> productsQuery = _context.Products.Include(p => p.Category).Where(p => p.Name.Contains(Test));
            //             var paginatedProducts = await PaginatedList<Product>.CreateAsync(productsQuery, pageNumber, 10);
            //             return View(paginatedProducts);
            //         }
            IQueryable<Service> servicesQuery = _context.Services.OrderByDescending(x => x.Status).ThenBy(x => x.ServiceName);
            var paginatedProducts = await PaginatedList<Service>.CreateAsync(servicesQuery, pageNumber, pageSize);
            return View(paginatedProducts);
        }

        // GET: Admin/Services/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }
        private async Task<string> SaveImage(IFormFile image,int id)
        {
            var uniqueFileName = $"{id}_{image.FileName}";
            var savePath = Path.Combine("wwwroot/images/services", uniqueFileName);

            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            return $"images/services/{uniqueFileName}"; // Trả về đường dẫn tương đối
        }
        // GET: Admin/Services/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ServiceName,Unit,Price,Quantity,Status,ImageUrl")] Service service, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                int maxId;
                if (_context.Services.Count() == 0)
                {
                    maxId = 0;
                }
                else
                {
                    maxId = _context.Services.Max(p => p.Id) + 1;

                }
                service.ImageUrl = await SaveImage(Image, maxId);
                _context.Add(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(service);
        }
       
        // GET: Admin/Services/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            _image = service.ImageUrl;
            return View(service);
        }

        // POST: Admin/Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ServiceName,Unit,Price,Quantity,Status,ImageUrl")] Service service,IFormFile? Image)
        {
            if (id != service.Id)
            {
                return NotFound();
            }
           
            if (ModelState.IsValid)
            {
                try
                {
                    if (Image == null)
                    {
                        service.ImageUrl = _image;
                    }
                    else
                    {
                        service.ImageUrl = await SaveImage(Image, service.Id);
                    }
                    _context.Update(service);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.Id))
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
            return View(service);
        }
        // GET: Admin/Services/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Admin/Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service != null)
            {
                _context.Services.Remove(service);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceExists(int id)
        {
            return _context.Services.Any(e => e.Id == id);
        }

        public async Task<IActionResult> SearchServices(string query, int pageNumber = 1)
        {
            IQueryable<Service> servicesQuery;

            if (string.IsNullOrEmpty(query))
            {
                servicesQuery = _context.Services.OrderByDescending(x => x.Status).ThenBy(x => x.ServiceName); 
            }
            else
            {
                servicesQuery = _context.Services.Where(p => p.ServiceName.Contains(query)).OrderByDescending(x => x.Status).ThenBy(x => x.ServiceName); ;

            }
            var paginatedProducts = await PaginatedList<Service>.CreateAsync(servicesQuery, pageNumber, 10);
            return PartialView("_ServiceSearchResult", paginatedProducts);
        }
        public List<string> SearchSuggestions(string query)
        {
            return _context.Services
            .Where(p => p.ServiceName.StartsWith(query))
            .Select(p => p.ServiceName)
            .ToList();

        }
    }
}
