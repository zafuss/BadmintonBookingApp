using BadmintonBookingApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BadmintonBookingApp.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public async Task<IActionResult> Index()
        {
            //var services = await _serviceRepository.GetAllAsync();
            UserModel _userModel = new UserModel(await _serviceRepository.GetAllAsync(), await _courtRepository.GetAllAsync());
            return View(_userModel);
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
    }
}
