using BadmintonBookingApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BadmintonBookingApp.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using BadmintonBookingApp.Models.Services;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using BadmintonBookingApp.Models.Padding;
using BadmintonBookingApp.Models.Nav;

namespace BadmintonBookingApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServiceRepository _serviceRepository;
        private readonly ICourtRepository _courtRepository;
        private readonly UserModel _userModel;
        public HomeController(ILogger<HomeController> logger, IServiceRepository serviceRepository, ICourtRepository courtRepository)
        {
            _logger = logger;
            _serviceRepository = serviceRepository;
            _courtRepository = courtRepository;
        }

        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            //var services = await _serviceRepository.GetAllAsync();
            IQueryable<Service> servicesQuery;
            servicesQuery = await _serviceRepository.GetServices();
            var paginatedServices = await PaginatedList<Service>.CreateAsync(servicesQuery, pageNumber, 8);
            UserModel _userModel = new UserModel(paginatedServices, await _courtRepository.GetAllAsync());
            ViewBag.CurrentPageName = "HomePage";
            return View(_userModel);
        }

        public async Task<IActionResult> IndexHome(int pageNumber = 1)
        {
            IQueryable<Service> servicesQuery;
            servicesQuery = await _serviceRepository.GetServices();
            var paginatedServices = await PaginatedList<Service>.CreateAsync(servicesQuery, pageNumber, 8);
            UserModel _userModel = new UserModel(paginatedServices, await _courtRepository.GetAllAsync());
            return PartialView("_IndexHome", _userModel);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public static int SlideValue { get; set; }

        [HttpPost]
        public ActionResult UpdateSlideValue(string slideValue)
        {
            Sidebar.curSlide = slideValue;
            return Json(new { success = true });
        }
    }
}
