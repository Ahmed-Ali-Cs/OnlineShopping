using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.DataAccess.Repositories.IRepository;
using OnlineShopping.Models;
using OnlineShopping.Models.Models;

namespace OnlineShopping.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork unitOfWork;

        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products=unitOfWork.Product.GetAll(Includeproperty:"Category").ToList();
            return View(products);
        }
        public IActionResult Details(int productId)
        {
            Product product = unitOfWork.Product.GetFirstOrDefault(p => p.Id == productId, Includeproperty:"Category");
            return View(product);
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
