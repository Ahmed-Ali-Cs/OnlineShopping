using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShopping.Data;
using OnlineShopping.DataAccess.Repositories.IRepository;
using OnlineShopping.Models;
using OnlineShopping.Models.Models;


namespace OnlineShopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var ProductList = unitOfWork.Product.GetAll();
            return View(ProductList);
        }

        public IActionResult Create()
        {
            IEnumerable<SelectListItem> CategoryList = unitOfWork.Category.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            ProductVM productVM = new()
            {
                Product = new Product(),
                CategoryList = CategoryList
            };
            return View(productVM);
        }

        [HttpPost]
        public IActionResult Create(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Product.Add(productVM.Product);
                unitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                productVM.CategoryList= unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
                return View(productVM);
            }
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? ProductFromdB = unitOfWork.Product.GetFirstOrDefault(c => c.Id == id);
            if (ProductFromdB == null)
            {
                return NotFound();
            }
            return View(ProductFromdB);
        }

        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Product.Update(obj);
                unitOfWork.Save();
                TempData["success"] = "Product updated successfully";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? ProductFromdB = unitOfWork.Product.GetFirstOrDefault(c => c.Id == id);
            if (ProductFromdB == null)
            {
                return NotFound();
            }
            return View(ProductFromdB);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Product? obj = unitOfWork.Product.GetFirstOrDefault(c => c.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            unitOfWork.Product.Remove(obj);
            unitOfWork.Save();
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction(nameof(Index));

        }
    }
}
