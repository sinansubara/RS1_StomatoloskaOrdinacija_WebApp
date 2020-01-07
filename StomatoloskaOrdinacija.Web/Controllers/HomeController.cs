using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StomatoloskaOrdinacija.Data;

namespace StomatoloskaOrdinacija.Web.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _db; 
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, MyContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
